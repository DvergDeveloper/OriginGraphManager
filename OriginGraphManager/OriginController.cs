using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace OriginGraphManager
{
    public static class OriginController
    {
        // Ключевые слова для поиска 
        static private readonly string _graphPageCodeWord = "Graph";
        static private readonly string _prodolCodeWord = "pr";
        static private readonly string _bokCodeWord = "bok";
        static private readonly string _shassiCodeWord = "sh";
        static private readonly string _txtSearchPattern = "*.txt";
        static private readonly string _txtCodeWord = ".txt";
        static private readonly string _newLineString = @"\";

        // Имена полей для записи
        static private readonly string _planeNameField = "PlaneName";
        static private readonly string _pageNameField = "PageName";
        static private readonly string _paramsDeskField = "Parameters";
        static private string[] _pageNames;

        // Переменные файла
        static private string _directoryName;
        static private List<Origin.GraphPage> _trueGraphPages;// = new List<Origin.GraphPage>();
        static private Origin.GraphPages _shablonGraphPages;// = org.GraphPages;


        static private bool _haveProdol = false;
        static private bool _haveBok = false;
        static private bool _haveShassi = false;
        static private bool _haveOthers = false;

        static private Origin.WorksheetPages _tempWorksheetPages = null;
        static private Origin.Worksheet _tempWorksheet = null;
        static private Origin.GraphObjects _textFields;

        // Переменные директории(папки)
        static private string[] _textFileNames;
        static private string[] _prodNames;
        static private string[] _bokNames;
        static private string[] _shassiNames;
        static private string[] _recordNames;
        static private int _allRecordsCount;
        static private string[] _OriginProjectNames;

        static public int AllRecordsCount { get { return _allRecordsCount; } }

        // Параметры для определения границ Х
        private delegate bool BoolFunc(double value, double targetValue);
        private static bool moreThen(double value, double targetValue) { return value > targetValue; }
        private static bool lessThen(double value, double targetValue) { return value < targetValue; }
        static private double[] _startTArray;
        static private double[] _startVarArray;
        static private double[] _endTArray;
        static private double[] _endVarArray;
        static private double _startBorder;
        static private double _endBorder;
        static private BoundsType _xBoundsType;

        static internal BoundsType XBoundsType { get { return _xBoundsType; } set { _xBoundsType = value; } }

        static private BoolFunc _startCondition;
        static private BoolFunc _endCondition;
        //static private string _moreThenString = ">";
        static private string _lessThenString = "<";

        // Приложение Origin
        static private Origin.Application _org;

        static public bool HaveProdol { get { return _haveProdol; } }
        static public bool HaveBok { get { return _haveBok; } }
        static public bool HaveShassi { get { return _haveShassi; } }
        static public bool HaveOthers { get { return _haveOthers; } }


        public static void Initialize()
        {
            _org = new Origin.Application();
            _org.NewProject();
            _org.Visible = Origin.MAINWND_VISIBLE.MAINWND_SHOW;
        }


        public static void LoadFile(string filefullName) // 
        {
            _org.Load(filefullName);
            //SetShablonVariables(filefullName);
        }

        public static void SetShablonVariables()
        {
            _trueGraphPages = new List<Origin.GraphPage>();
            _shablonGraphPages = _org.GraphPages;
            DefineTrueGraphPages();
            _pageNames = new string[_trueGraphPages.Count];
            DefineAvailableChannels();
        }
        //==================
        public static void SetDerictoryVariables(string filefullName)
        {
            _directoryName = Path.GetDirectoryName(filefullName);
            _textFileNames = Directory.GetFileSystemEntries(_directoryName, _txtSearchPattern);
        }

        public static void DefineAvailableChannels()
        {
            _haveProdol = _haveBok = _haveShassi = _haveOthers = false;

            for (int i = 0; i < _trueGraphPages.Count; i++)
            {
                if (_trueGraphPages[i].LongName.ToLower().Contains("pr"))
                    _haveProdol = true;
                else if (_trueGraphPages[i].LongName.ToLower().Contains("bok"))
                    _haveBok = true;
                else if (_trueGraphPages[i].LongName.ToLower().Contains("sh"))
                    _haveShassi = true;
                else
                    _haveOthers = true;
            }
        }

        public static void DefineTrueGraphPages()
        {
            for (int i = 0; i < _shablonGraphPages.Count; i++)
                if (_shablonGraphPages[i].Name.Contains(_graphPageCodeWord))
                    _trueGraphPages.Add(_shablonGraphPages[i]);
        }

        public static void FillAllNameArrays()
        {
            _prodNames = _haveProdol ? GetAllFileNames(_textFileNames, RecordTypes.prodol) : new string[0];
            _bokNames = _haveBok ? GetAllFileNames(_textFileNames, RecordTypes.bok) : new string[0];
            _shassiNames = _haveShassi ? GetAllFileNames(_textFileNames, RecordTypes.shassi) : new string[0];

            if (!_haveProdol && !_haveBok && !_haveShassi)
                _recordNames = _textFileNames;
            else if (_textFileNames.Length > _prodNames.Length + _bokNames.Length + _shassiNames.Length)
                _recordNames = GetAllFileNames(_textFileNames, RecordTypes.other);
            else
                _recordNames = new string[0];
        }

        public static bool IsCorrectFilesCount()
        {
            if (_haveProdol && _haveBok && _haveShassi)
            {
                if (_prodNames.Length != _bokNames.Length || _prodNames.Length != _shassiNames.Length || _bokNames.Length != _shassiNames.Length)
                {
                    MessageBox.Show("Количество записей prodol и bok и shassi не совпадает");
                    return false;
                }
            }
            else if (_haveProdol && _haveBok && !_haveShassi)
            {
                if (_prodNames.Length != _bokNames.Length)
                {
                    MessageBox.Show("Количество записей prodol и bok не совпадает");
                    return false;
                }
            }
            else if (_haveProdol && !_haveBok && _haveShassi)
            {
                if (_prodNames.Length != _shassiNames.Length)
                {
                    MessageBox.Show("Количество записей prodol и shassi не совпадает");
                    return false;
                }
            }
            else if (!_haveProdol && _haveBok && _haveShassi)
            {
                if (_bokNames.Length != _shassiNames.Length)
                {
                    MessageBox.Show("Количество записей bok и shassi не совпадает");
                    return false;
                }
            }

            if (_textFileNames.Length > _prodNames.Length + _bokNames.Length + _shassiNames.Length
               && (_haveBok || _haveProdol || _haveShassi))
            {
                if (_recordNames.Length != Math.Max(Math.Max(_prodNames.Length, _bokNames.Length), _shassiNames.Length))
                {
                    MessageBox.Show("Количество записей дополнительного типа не совпадает");
                    return false;
                }
            }

            return true;
        }

        public static void SortNameArrays()
        {
            if (_haveProdol)
                _prodNames = _prodNames.OrderBy(x => x).ToArray();

            if (_haveBok)
                _bokNames = _bokNames.OrderBy(x => x).ToArray();

            if (_haveShassi)
                _shassiNames = _shassiNames.OrderBy(x => x).ToArray();

            if (_textFileNames.Length > _prodNames.Length + _bokNames.Length + _shassiNames.Length)
                _recordNames = _recordNames.OrderBy(x => x).ToArray();
        }

        public static void CreateOriginProjectNames()
        {
            if (!_haveBok && !_haveProdol && !_haveShassi)
                _OriginProjectNames = FreeStringsFromElements(_recordNames, new string[] { _directoryName, _newLineString, _txtCodeWord });
            else if (_haveProdol)
                _OriginProjectNames = FreeStringsFromElements(_prodNames, new string[] { _directoryName, _newLineString, _txtCodeWord, _prodolCodeWord });
            else if (_haveBok)
                _OriginProjectNames = FreeStringsFromElements(_bokNames, new string[] { _directoryName, _newLineString, _txtCodeWord, _bokCodeWord });
            else
                _OriginProjectNames = FreeStringsFromElements(_shassiNames, new string[] { _directoryName, _newLineString, _txtCodeWord, _shassiCodeWord });

            _allRecordsCount = _OriginProjectNames.Length;
        }

        public static void GetOriginProjectNames(out string[] OriginProjectNames)
        {
            OriginProjectNames = new string[_OriginProjectNames.Length];

            for (int i = 0; i < _OriginProjectNames.Length; i++)
                OriginProjectNames[i] = _OriginProjectNames[i];
        }
        public static void SetXBorders(double fixXStart, double fixXEnd)
        {
            _startBorder = fixXStart;
            _endBorder = fixXEnd;
        }

        public static bool TryCalculateXBounds(int i_record, string[] startChoosedNames, string[] endChoosedNames,
            int startParamIndex, int endParamIndex, double startTargetValue, double endTargetValue, string startConditionString, string endConditionString)
        {
            _startCondition = (startConditionString == _lessThenString) ? (BoolFunc)lessThen : (BoolFunc)moreThen;
            _endCondition = (endConditionString == _lessThenString) ? (BoolFunc)lessThen : (BoolFunc)moreThen;

            _startTArray = GetColumnFromTXT(startChoosedNames[i_record], GetParamNamesFromTXT(startChoosedNames[i_record]), 0);
            _startVarArray = GetColumnFromTXT(startChoosedNames[i_record], GetParamNamesFromTXT(startChoosedNames[i_record]), startParamIndex);
            _endTArray = GetColumnFromTXT(endChoosedNames[i_record], GetParamNamesFromTXT(endChoosedNames[i_record]), 0);
            _endVarArray = GetColumnFromTXT(endChoosedNames[i_record], GetParamNamesFromTXT(endChoosedNames[i_record]), endParamIndex);

            return TryGetTimeOfTrue(_startTArray, _startVarArray, startTargetValue, _startCondition, SearchDirection.fromStart, out _startBorder) &&
            TryGetTimeOfTrue(_endTArray, _endVarArray, endTargetValue, _endCondition, SearchDirection.fromEnd, out _endBorder);
        }

        public static void FillWorksheet(int i_record)
        {
            string tempWorksheetName;

            for (int i_sheetPages = 0; i_sheetPages < _org.WorksheetPages.Count; i_sheetPages++)
            {
                _tempWorksheetPages = _org.WorksheetPages;
                _tempWorksheet = (Origin.Worksheet)_tempWorksheetPages[i_sheetPages].Layers[0];
                tempWorksheetName = _org.WorksheetPages[i_sheetPages].LongName.ToLower();

                if (tempWorksheetName.Contains(_prodolCodeWord))
                {
                    WriteFromTXTtoWorksheet(_prodNames[i_record], _tempWorksheet);
                    _org.WorksheetPages[i_sheetPages].LongName = _prodolCodeWord + _OriginProjectNames[i_record];
                }
                else if (tempWorksheetName.Contains(_bokCodeWord))
                {
                    WriteFromTXTtoWorksheet(_bokNames[i_record], _tempWorksheet);
                    _org.WorksheetPages[i_sheetPages].LongName = _bokCodeWord + _OriginProjectNames[i_record];
                }
                else if (tempWorksheetName.Contains(_shassiCodeWord))
                {
                    WriteFromTXTtoWorksheet(_shassiNames[i_record], _tempWorksheet);
                    _org.WorksheetPages[i_sheetPages].LongName = _shassiCodeWord + _OriginProjectNames[i_record];
                }
                else
                {
                    WriteFromTXTtoWorksheet(_recordNames[i_record], _tempWorksheet);
                    _org.WorksheetPages[i_sheetPages].LongName = _OriginProjectNames[i_record];
                }
            }
        }

        public static void FillOriginTextFields(string planeName, string pageName, string paramsDesk)
        {
            for (int i_GraphPage = 0; i_GraphPage < _trueGraphPages.Count; i_GraphPage++)
            {
                for (int i_Layer = 0; i_Layer < _trueGraphPages[i_GraphPage].Layers.Count; i_Layer++)
                {
                    _textFields = ((Origin.GraphLayer)_trueGraphPages[i_GraphPage].Layers[i_Layer]).GraphObjects;

                    for (int i_Objects = 0; i_Objects < _textFields.Count; i_Objects++)
                    {
                        if (_textFields[i_Objects].Name == _planeNameField)
                        {
                            _textFields[i_Objects].Text = planeName;
                        }
                        else if (_textFields[i_Objects].Name == _pageNameField)
                        {
                            _textFields[i_Objects].Text = pageName;
                        }
                        else if (_textFields[i_Objects].Name == _paramsDeskField)
                        {
                            _textFields[i_Objects].Text = paramsDesk;
                        }
                    }
                }
            }
        }

        public static void RescaleXAxis(bool needCorrection)
        {
            RescaleX(_org, _startBorder, _endBorder, needCorrection);
        }

        public static void RescaleYAxis()
        {
            RescaleY(_org);
        }

        public static void SaveProject(int i_record)
        {
            _org.Save(_directoryName + _newLineString + _OriginProjectNames[i_record]);
        }

        public static void ExitOrigin()
        {
            _org.Exit();
        }

        public static void ReleaseCOMObjects()
        {
            Marshal.ReleaseComObject(_tempWorksheetPages);
            Marshal.ReleaseComObject(_tempWorksheet);
            Marshal.ReleaseComObject(_textFields);
            Marshal.ReleaseComObject(_shablonGraphPages);
            Marshal.ReleaseComObject(_org);
        }


        enum RecordTypes
        {
            prodol,
            bok,
            shassi,
            other
        }

        enum SearchDirection
        {
            fromStart,
            fromEnd
        }

        #region [basic methods]

        public static void GetProdolNames(out string[] prodolNames)
        {
            prodolNames = new string[_prodNames.Length];

            for (int i = 0; i < _prodNames.Length; i++)
                prodolNames[i] = _prodNames[i];
        }

        public static void GetBokNames(out string[] bokNames)
        {
            bokNames = new string[_bokNames.Length];

            for (int i = 0; i < _bokNames.Length; i++)
                bokNames[i] = _bokNames[i];
        }

        public static void GetShassiNames(out string[] shassiNames)
        {
            shassiNames = new string[_prodNames.Length];

            for (int i = 0; i < _prodNames.Length; i++)
                shassiNames[i] = _prodNames[i];
        }

        public static void GetOtherNames(out string[] otherNames)
        {
            otherNames = new string[_prodNames.Length];

            for (int i = 0; i < _prodNames.Length; i++)
                otherNames[i] = _prodNames[i];
        }

        #region [Origin methods]

        private static void WriteFromTXTtoWorksheet(string txtPath, Origin.Worksheet orgWks)
        {
            //List<string> elements = new List<string>();
            string[] ClearedElements = GetClearedElements(txtPath);

            int paramsCount = GetColumnCount(ClearedElements);

            string[] paramNames = GetParamNames(ClearedElements, paramsCount);


            string[] tempStringArr = new string[paramsCount];
            List<string[]> dataList = new List<string[]>();

            int column = 0;

            for (int i = paramNames.Length; i < ClearedElements.Length; i++)
            {
                tempStringArr[column] = ClearedElements[i];

                if (column + 1 >= paramsCount)
                {
                    dataList.Add(tempStringArr);
                    column = 0;
                    tempStringArr = new string[paramsCount];
                }
                else
                {
                    column++;
                }
            }

            string[,] data = new string[dataList.Count, dataList[0].Length];

            for (int i = 0; i < dataList.Count; i++)
            {
                for (int j = 0; j < dataList[0].Length; j++)
                {
                    data[i, j] = dataList[i][j];
                }
            }

            // ДОБАВЛЯЕМ КОЛОННЫ В SHEET
            if (orgWks.Columns.Count != paramsCount)
                for (int i = orgWks.Columns.Count - 1; i < paramsCount; i++)
                    orgWks.Columns.Add(System.Type.Missing);

            for (int i = 0; i < paramsCount; i++)
                orgWks.Columns[i].LongName = paramNames[i];

            orgWks.SetData(data);
        }

        #endregion
        //Xbounds

        private static bool TryGetTimeOfTrue(double[] tArray, double[] varArray, double targetValue, BoolFunc targetFunc, SearchDirection searchDirection, out double time)
        {
            int startIndex, endIndex, step;

            if (searchDirection == SearchDirection.fromStart)
            {
                startIndex = 0;
                endIndex = varArray.Length - 1;
                step = 1;
            }
            else
            {
                startIndex = varArray.Length - 1;
                endIndex = 0;
                step = -1;
            }

            for (int i = startIndex; i != endIndex; i += step)
            {
                if (targetFunc(varArray[i], targetValue))
                {
                    time = tArray[i];
                    return true;
                }
            }

            time = 0;
            return false;// ХЗ что лучше добавлять
        }


        //==================
        public static string[] GetParamNamesFromTXT(string txtPath)
        {
            string[] ClearedElements = GetClearedElements(txtPath);
            int paramsCount = GetColumnCount(ClearedElements);

            return GetParamNames(ClearedElements, paramsCount);
        }

        private static string[] GetParamNames(string[] ClearedElements, int paramsCount)
        {
            string[] paramNames = new string[paramsCount];

            for (int i = 0; i < paramsCount; i++)
                paramNames[i] = ClearedElements[i].Replace("\n", "").Replace("\r", "");

            return paramNames;
        }

        private static int GetColumnCount(string[] ClearedElements)
        {
            double tempDouble;
            int paramsCount = 0;
            // определяем количество столбцов в файле
            for (int i = 0; i < ClearedElements.Length; i++)
            {
                if (Double.TryParse(ClearedElements[i], out tempDouble))
                {
                    paramsCount = i;
                    break;
                }
            }
            return paramsCount;
        }

        private static double[] GetColumnFromTXT(string txtPath, string[] paramNames, int targetParamIndex)
        {
            string[] ClearedElements = GetClearedElements(txtPath);

            int paramsCount = paramNames.Length;

            string[] tempStringArr = new string[paramsCount];
            List<string[]> dataList = new List<string[]>();

            int column = 0;

            for (int i = paramNames.Length; i < ClearedElements.Length; i++)
            {
                tempStringArr[column] = ClearedElements[i];

                if (column + 1 >= paramsCount)
                {
                    dataList.Add(tempStringArr);
                    column = 0;
                    tempStringArr = new string[paramsCount];
                }
                else
                {
                    column++;
                }
            }

            string[,] data = new string[dataList.Count, dataList[0].Length];

            for (int i = 0; i < dataList.Count; i++)
            {
                for (int j = 0; j < dataList[0].Length; j++)
                {
                    data[i, j] = dataList[i][j];
                }
            }

            double[] targetColumn = new double[dataList.Count - 1];

            for (int i = 0; i < dataList.Count - 1; i++)
            {
                try
                {
                    targetColumn[i] = Double.Parse(data[i + 1, targetParamIndex], System.Globalization.CultureInfo.InvariantCulture);
                    //Console.WriteLine(data[i + 1, targetParamIndex]);
                }
                catch
                {
                    //Console.WriteLine(data[i + 1, targetParamIndex]);
                    Environment.Exit(0);
                }
            }


            return targetColumn;
        }

        private static string[] GetClearedElements(string txtPath)
        {
            string[] newLine = new string[] { Environment.NewLine, " " };

            string stringData = System.IO.File.ReadAllText(txtPath);
            string[] splitData = stringData.Split(newLine, StringSplitOptions.RemoveEmptyEntries);//.Split(' ');
            string[] ClearedElements = splitData.Where(p => p != " ").Where(p => p != "").Where(p => p != Environment.NewLine).ToArray();
            return ClearedElements;
        }

        //==================

        private static string[] GetAllFileNames(string[] textFileNames, RecordTypes recordType)
        {
            switch (recordType)
            {
                case RecordTypes.prodol:
                    return textFileNames.Where(x => Path.GetFileName(x).StartsWith("pr")).ToArray();
                case RecordTypes.bok:
                    return textFileNames.Where(x => Path.GetFileName(x).StartsWith("bok")).ToArray();
                case RecordTypes.shassi:
                    return textFileNames.Where(x => Path.GetFileName(x).StartsWith("sh")).ToArray();
                default:
                    return textFileNames
                .Except(textFileNames.Where(x => Path.GetFileName(x).StartsWith("pr")))
                .Except(textFileNames.Where(x => Path.GetFileName(x).StartsWith("bok")))
                .Except(textFileNames.Where(x => Path.GetFileName(x).StartsWith("sh"))).ToArray();
            }
        }

        #region [Rescale]
        private static void RescaleX(Origin.Application org, double startBorder, double endBorder, bool needCorrection)
        {

            org.Execute("run.LoadOC(RescaleAllX,[option]);");
            org.Execute("XRescaleGraphs " + startBorder.ToString(System.Globalization.CultureInfo.InvariantCulture) + " "
                + endBorder.ToString(System.Globalization.CultureInfo.InvariantCulture) + " " + (needCorrection ? 2d : -2d).ToString() + " ;");
        }

        private static void RescaleY(Origin.Application org)
        {
            org.Execute("run.LoadOC(RescaleAllGraphs,[option]);");
            org.Execute("RescaleGraphs;");
        }
        #endregion

        private static string[] FreeStringsFromElements(string[] array, string[] element)
        {
            string[] result = new string[array.Length];

            for (int i = 0; i < array.Length; i++)
            {
                result[i] = array[i];
            }

            for (int i = 0; i < result.Length; i++)
            {
                for (int j = 0; j < element.Length; j++)
                {
                    result[i] = result[i].Replace(element[j], "");
                }
            }

            return result;
        }

        private static void ReleaseComObjects(Origin.GraphPages shablonGraphPages, Origin.Application org)
        {
            org.Exit();
            Marshal.ReleaseComObject(shablonGraphPages);
            Marshal.ReleaseComObject(org);
        }
        #endregion
    }
}
