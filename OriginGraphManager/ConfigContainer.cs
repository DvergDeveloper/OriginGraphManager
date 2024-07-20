using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace OriginGraphManager
{
    public static class ConfigContainer
    {
        // текст для полей
        static private string _planeName;
        static private bool _isVariantPageName;
        static private string _pageName;
        static private string[] _pageNames;
        static private string _paramsDesk;

        static private bool _haveVmark;
        static private string _Vmark;

        // получение текстов полей извне
        static public string PlaneName { get { return _planeName; } }
        static public bool IsVariantPageName { get { return _isVariantPageName; } }
        static public string PageName { get { return _pageName; } }
        static public string ParamsDesk { get { return _paramsDesk; } }

        static public bool HaveVmark { get { return _haveVmark; } }
        static public string Vmark { get { return _Vmark; } }

        // вариативность параметров полета
        static private bool _isGVariant;
        static private bool _isXTVariant;
        static private bool _isDZVariant;
        static private bool _isDPVariant;
        static private bool _isVprVariant;
        static private bool _isWzVariant;
        static private bool _isTextVariant;

        // значения вариативных параметров
        static private string[] _TextVariation;
        static private string[] _GVariation;
        static private string[] _XTVariation;
        static private string[] _DZVariant;
        static private string[] _DPVariant;
        static private string[] _VprVariation;
        static private string[] _WzVariation;

        // Параметры для определения границы Х
        private delegate bool BoolFunc(double value, double targetValue);

        static private int _startParamIndex;
        static private int _endParamIndex;
        static private string _startCondition;
        static private string _endCondition;
        static private double _startTargetValue;
        static private double _endTargetValue;
        static private string[] _startChoosedNames;
        static private string[] _endChoosedNames;
        static private bool _needCorrection;
        static internal BoundsType _xBoundType;
        static private double _fixXStartBound;
        static private double _fixXEndBound;

        // Получение параметров ИЗВНЕ
        static public double FixXStartBound { get { return _fixXStartBound; } }
        static public double FixXEndBound { get { return _fixXEndBound; } }
        static public int StartParamIndex { get { return _startParamIndex; } }
        static public int EndParamIndex { get { return _endParamIndex; } }
        static public string StartCondition { get { return _startCondition; } }
        static public string EndCondition { get { return _endCondition; } }
        static public double StartTargetValue { get { return _startTargetValue; } }
        static public double EndTargetValue { get { return _endTargetValue; } }
        static public bool NeedCorrection { get { return _needCorrection; } }
        // Копирование массива
        static public string[] StartChoosedNames
        {
            get
            {
                string[] tempStart = new string[_startChoosedNames.Length];
                _startChoosedNames.CopyTo(tempStart, 0);
                return tempStart;
            }
        }
        static public string[] EndChoosedNames
        {
            get
            {
                string[] tempEnd = new string[_endChoosedNames.Length];
                _endChoosedNames.CopyTo(tempEnd, 0);
                return tempEnd;
            }
        }

        public static void SetDataFromForm(string planeName, bool isVariantPageName,
        string pageName, bool haveVmark, string Vmark)
        {
            _planeName = planeName;
            _isVariantPageName = isVariantPageName;
            _pageName = pageName;
            _haveVmark = haveVmark;
            _Vmark = Vmark;
        }

        public static void SetXBoundsParams(int startParamIndex, int endParamIndex, string startConditionString,
         string endConditionString, double startTargetValue, double endTargetValue, string[] startChoosedNames,
         string[] endChoosedNames, bool needCorrection, BoundsType boundsType, double fixStartBound, double fixEndBound)
        {
            _startParamIndex = startParamIndex;
            _endParamIndex = endParamIndex;
            _startCondition = startConditionString;//(startConditionString == _lessThenString) ? (BoolFunc)lessThen : (BoolFunc)moreThen;
            _endCondition = endConditionString;//(endConditionString == _lessThenString) ? (BoolFunc)lessThen : (BoolFunc)moreThen;
            _startTargetValue = startTargetValue;
            _endTargetValue = endTargetValue;
            _startChoosedNames = startChoosedNames;
            _endChoosedNames = endChoosedNames;
            _needCorrection = needCorrection;
            _xBoundType = boundsType;
            _fixXStartBound = fixStartBound;
            _fixXEndBound = fixEndBound;
        }

        public static void SetRezhimParamsFromTable(List<string> columnG, List<string> columnXT, List<string> columnDZ,
        List<string> columnDP, List<string> columnVpr, List<string> columnWz, List<string> columnText, List<string> columnPageNames)
        {
            _isGVariant = CheckAndFillVariation(columnG, out _GVariation);
            _isXTVariant = CheckAndFillVariation(columnXT, out _XTVariation);
            _isDZVariant = CheckAndFillVariation(columnDZ, out _DZVariant);
            _isDPVariant = CheckAndFillVariation(columnDP, out _DPVariant);
            _isVprVariant = CheckAndFillVariation(columnVpr, out _VprVariation);
            _isWzVariant = CheckAndFillVariation(columnWz, out _WzVariation);
            _isTextVariant = CheckAndFillVariation(columnText, out _TextVariation);
            _isVariantPageName = CheckAndFillVariation(columnPageNames, out _pageNames);
        }

        public static string GetPageNameByIndex(int i_record)
        {
            return _pageNames[i_record];
        }

        public static void CreateParamsDesk(int i_record)
        {
            string delimeter = ", ";
            string parametersField = "";

            if (_isTextVariant)
                parametersField += _TextVariation[i_record] + delimeter;

            if (_isGVariant)
                parametersField += "G = " + _GVariation[i_record] + " т." + delimeter;

            if (_isXTVariant)
                parametersField += @"X\-(Т) = " + _XTVariation[i_record] + "% CAX" + delimeter;

            if (_isDZVariant)
                parametersField += @"\g(d) \-(З) = " + _DZVariant[i_record] + @"\g(\(a176))" + delimeter;

            if (_isDPVariant)
                parametersField += @"\g(d) \-(ПР) = " + _DPVariant[i_record] + @"\g(\(a176))" + delimeter;

            if (_isVprVariant)
                parametersField += @"V\-(" + (_haveVmark ? _Vmark : "") + ")= " + _VprVariation[i_record] + " км/ч ПР" + delimeter;

            if (_isWzVariant)
                parametersField += @"W\-(z) = " + _WzVariation[i_record] + " м/с";

            if (parametersField.Length >= 2)
                if (parametersField.Substring(parametersField.Length - 2, 2) == delimeter)
                    parametersField = parametersField.Substring(0, parametersField.Length - 2);

            _paramsDesk = parametersField;
        }

        private static bool CheckAndFillVariation(List<string> variation, out string[] result)
        {
            if (variation.Count < 1)
            {
                result = new string[0];
                return false;
            }
            else
            {
                result = variation.ToArray();
                return true;
            }
        }

        #region [basic methods]

        public static bool CheckTableInput()
        {
            List<string[]> columns = new List<string[]>();
            int columnsLength;

            if (_isTextVariant)
                columns.Add(_TextVariation);
            if (_isGVariant)
                columns.Add(_GVariation);
            if (_isXTVariant)
                columns.Add(_XTVariation);
            if (_isDZVariant)
                columns.Add(_DZVariant);
            if (_isDPVariant)
                columns.Add(_DPVariant);
            if (_isVprVariant)
                columns.Add(_VprVariation);
            if (_isWzVariant)
                columns.Add(_WzVariation);

            if (columns.Count > 0)// возможно следует переделать
            {
                columnsLength = columns[0].Length;

                for (int i = 1; i < columns.Count; i++)
                {
                    if (columnsLength != columns[i].Length)
                    {
                        System.Windows.Forms.MessageBox.Show("Таблица заполнена неправильно");
                        columns.Clear();
                        return false;
                    }
                }
            }

            return true;
        }



        public static bool IsCorrectNumericInput(KeyPressEventArgs e, TextBox textBox)
        {
            if (e.KeyChar == '.')
                e.KeyChar = ',';

            if (e.KeyChar >= '0' && e.KeyChar <= '9')
            {
                return true;
            }
            else if (e.KeyChar == ',')
            {
                if (textBox.Text.IndexOf(',') != -1)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else if (Char.IsControl(e.KeyChar) ||
                e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Delete)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion
    }
}
