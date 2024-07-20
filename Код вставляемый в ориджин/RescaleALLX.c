/*------------------------------------------------------------------------------*
 * File Name:				 													*
 * Creation: 																	*
 * Purpose: OriginC Source C file												*
 * Copyright (c) ABCD Corp.	2003, 2004, 2005, 2006, 2007, 2008, 2009, 2010		*
 * All Rights Reserved															*
 * 																				*
 * Modification Log:															*
 *------------------------------------------------------------------------------*/
 
////////////////////////////////////////////////////////////////////////////////////
// Including the system header file Origin.h should be sufficient for most Origin
// applications and is recommended. Origin.h includes many of the most common system
// header files and is automatically pre-compiled when Origin runs the first time.
// Programs including Origin.h subsequently compile much more quickly as long as
// the size and number of other included header files is minimized. All NAG header
// files are now included in Origin.h and no longer need be separately included.
//
// Right-click on the line below and select 'Open "Origin.h"' to open the Origin.h
// system header file.
#include <Origin.h>
////////////////////////////////////////////////////////////////////////////////////

//#pragma labtalk(0) // to disable OC functions for LT calling.

////////////////////////////////////////////////////////////////////////////////////
// Include your own header files here.


////////////////////////////////////////////////////////////////////////////////////
// Start your functions here.


int XRescaleGraphs(double startBorder, double endBorder, double NeedCorrectBorders)
{
GraphPage page;	

int lastIndex, startIndex, endIndex;

foreach (GraphPage grPage in Project.GraphPages)
{

char *istr;
istr = strstr(grPage.GetName(),"Graph");
	
if (istr != NULL)
{
	
page = grPage;

//GraphLayer
foreach(GraphLayer grLayer in page.Layers)
{
	
Scale XgraphScale(grLayer.X);

//====================================

double someArray[2];
someArray[0]=startBorder;
someArray[1]=endBorder;
double someArraySize =2;

// Calculate borders and ticks===========================
double toOne = XGetToOneCoeff(someArray,someArraySize); // zamena /10 -> nothing

double buttomBorder;
double upBorder;

if (NeedCorrectBorders>0)
{
  buttomBorder = XGetButtomBorder(someArray,someArraySize,toOne/10);  // zamena /10 -> nothing
  upBorder = XGetUpBorder(someArray,someArraySize,toOne/10);          // zamena /10 -> nothing
}
else
{
  buttomBorder = XGetMin(someArray,someArraySize);
  upBorder = XGetMax(someArray,someArraySize);
}


double ticks[6];
ticks[0]=0.01*toOne;
ticks[1]=0.05*toOne;
ticks[2]=0.1*toOne;
ticks[3]=0.5*toOne;
ticks[4]=1*toOne;
ticks[5]=5*toOne;   

double ticksCount[6];
ticksCount[0]=(upBorder-buttomBorder)/ticks[0];
ticksCount[1]=(upBorder-buttomBorder)/ticks[1];
ticksCount[2]=(upBorder-buttomBorder)/ticks[2];
ticksCount[3]=(upBorder-buttomBorder)/ticks[3];
ticksCount[4]=(upBorder-buttomBorder)/ticks[4];
ticksCount[5]=(upBorder-buttomBorder)/ticks[5];


int minorTicksCount[]={10,5,10,5,10,5};

double optTick;
int optMinorTicksCount;

if (upBorder<100)
{
  for (int i=0;i<6;i++)
  {
    if (ticksCount[i] <= 20 && 4 < ticksCount[i])
    {
    optTick=ticks[i];
    optMinorTicksCount=minorTicksCount[i];
    break;
    }
  }
}
else
{
  for (int i=0;i<6;i++)
  {
    if (ticksCount[i] < 20 && 4 <= ticksCount[i])
    {
    optTick=ticks[i];
    optMinorTicksCount=minorTicksCount[i];
    break;
    }
  }
}	


if (NeedCorrectBorders>0)
{
buttomBorder = XExtendBorder( buttomBorder, optTick, -1, toOne, optMinorTicksCount);
upBorder     = XExtendBorder( upBorder,     optTick,  1, toOne, optMinorTicksCount);

buttomBorder = XReduceBorder( buttomBorder, optTick, -1,  someArray[0],  someArray[1]);
upBorder     = XReduceBorder( upBorder,     optTick,  1,  someArray[0],  someArray[1]);
}

//buttomBorder = XPushLineFromBorder( buttomBorder, optTick, -1,  someArray[0],  someArray[1]);
//upBorder     = XPushLineFromBorder( upBorder,     optTick,  1,  someArray[0],  someArray[1]);


// IA DOBAVIL UDOLI ESLI CHTO
ticksCount[0]=(upBorder-buttomBorder)/ticks[0];
ticksCount[1]=(upBorder-buttomBorder)/ticks[1];
ticksCount[2]=(upBorder-buttomBorder)/ticks[2];
ticksCount[3]=(upBorder-buttomBorder)/ticks[3];
ticksCount[4]=(upBorder-buttomBorder)/ticks[4];
ticksCount[5]=(upBorder-buttomBorder)/ticks[5];


if (upBorder<100)
{
  for (int J=0;J<6;J++)
  {
    if (20 >= ticksCount[J] && 4 < ticksCount[J])
    {
    optTick=ticks[J];
    optMinorTicksCount=minorTicksCount[J];
    break;
    }
  }
}
else
{
  for (int J=0;J<6;J++)
  {
    if (20 > ticksCount[J] && 4 <= ticksCount[J])
    {
    optTick=ticks[J];
    optMinorTicksCount=minorTicksCount[J];
    break;
    }
  }
}


if (NeedCorrectBorders>0)
{
buttomBorder = XExtendBorder( buttomBorder, optTick, -1, toOne, optMinorTicksCount);
upBorder     = XExtendBorder( upBorder,     optTick,  1, toOne, optMinorTicksCount);

buttomBorder = XReduceBorder( buttomBorder, optTick, -1,  someArray[0],  someArray[1]);
upBorder     = XReduceBorder( upBorder,     optTick,  1,  someArray[0],  someArray[1]);
}
//========================================================================================
	
// KASTIL X<0
if (NeedCorrectBorders>0)
{
if (buttomBorder<0)
{
buttomBorder=0;
}
}

XgraphScale.From=buttomBorder;
XgraphScale.To=upBorder;
XgraphScale.Inc=optTick;

if (optMinorTicksCount>7)
{
grLayer.LT_execute("axis -ps X M 1");
}
else
{
grLayer.LT_execute("axis -ps X M 4");
}

grLayer.LT_execute("doc -uw");
}

}

}

return 1;
}

// DataPlot methods====================================

int XGetLastIndex(DataPlot plot)
{
double xTemp,yTemp;
int index=0;
bool boolres;

for(int i=0;i<100000;i++)
{
boolres = plot.GetDataPoint(i,&xTemp,&yTemp);
if(boolres)
{
index=i;
}
else
{	
break;
}
} 

return index;
}

int XGetIndexOfStart(DataPlot plot,int lastIndex,double from)
{
double xTemp,yTemp;

for (int i=0;i<lastIndex;i++)
{
plot.GetDataPoint(i,&xTemp,&yTemp);

if(from<xTemp)
{
return i-1;
}
}

return -1;
}

int XGetIndexOfEnd(DataPlot plot,int lastIndex,double to)
{
double xTemp,yTemp;

for (int i=lastIndex-1;i>0;i--)
{
plot.GetDataPoint(i,&xTemp,&yTemp);

if(to>xTemp)
{
return i+1;
}
}

return -1;
}

double XGetMaxLocalY(DataPlot plot,int from,int to)
{
double xTemp,yTemp;
plot.GetDataPoint(from,&xTemp,&yTemp);
double max=0;
max=yTemp;

for (int i=from;i<to+1;i++)
{
plot.GetDataPoint(i,&xTemp,&yTemp);

if (yTemp>max)
{
max=yTemp;
}
}

return max;
}

double XGetMinLocalY(DataPlot plot,int from,int to)
{
double xTemp,yTemp;
plot.GetDataPoint(from,&xTemp,&yTemp);
double min=0;
min=yTemp;

for (int i=from;i<to+1;i++)
{
plot.GetDataPoint(i,&xTemp,&yTemp);

if (yTemp<min)
{
min=yTemp;
}
}

return min;
}
//=====================================================

double XGetToOneCoeff(double array[],double sizeArray)
{
return (double)pow(10,XGetNumberOrder(XGetMaxAbs(array,sizeArray)));
}

double XGetButtomBorder(double array[],double sizeArray, double toOne)
{
return ceil(XGetMin(array,sizeArray)/toOne-1.01)*toOne; //zamena 1.01
}

double XGetUpBorder(double array[],double sizeArray, double toOne)
{
return floor(XGetMax(array,sizeArray)/toOne+1.01)*toOne;//zamena 1.01
}

double XGetTickCase(double ButtomBorder,double UpBorder,double toOne)
{
return (UpBorder-ButtomBorder)/toOne;
}

double XGetTick(double toOne,double tickCase)
{
  if (tickCase>7)
  {
    return toOne*5.0;
  }
  else if(tickCase>4)
  {
    return toOne;
  }
  else
  {
    return toOne*0.5;
  }
}

double XPushLineFromBorder(double border,double tick,double direction, double min, double max)
{
double newBorder = border;

if (direction>0)
{
if ((border-max)<tick/5)
{
newBorder = border+tick;
}
}
else
{
if ((min-border)<tick/5)
{
newBorder = border-tick;
}
}

return newBorder;
}

double XReduceBorder(double border,double tick,double direction, double min, double max)
{
double reducedBorder = border;

 if(direction>0)
 {
   if ((border-tick)>max)
   {
   reducedBorder = border-tick;
   }
 }
 else
 {
   if ((border+tick)<min)
   {
   reducedBorder = border+tick;
   }
 }
 
 return reducedBorder;
}


double XExtendBorder(double border,double tick,double direction, double toOne,int minorTicksCount)
{
double nextBorder = border;

for(int i=0;i<minorTicksCount;i++)
{
  if (fabs(Xmod(nextBorder,tick,toOne))<pow(10,-10)*tick/minorTicksCount)
  {
	border=nextBorder;
    break;
  }
  else
  {
    nextBorder = nextBorder+direction*tick/minorTicksCount;	
  }
}

return nextBorder;
}

int Xmod(double num1,double num2, double toOne)
{
int intNum1=(int)round((num1/toOne*10),0);
int intNum2=(int)round((num2/toOne*10),0);

return intNum1%intNum2;
}

double XGetMaxAbs(double array[],double arraySize)
{
double maxAbs=fabs(array[0]);

for (int i=0;i<arraySize;i++)
{	
  if(fabs(array[i])>maxAbs)
  {
  maxAbs=fabs(array[i]);
  }
}

return maxAbs;
}

double XGetMax(double array[],double arraySize)
{
double max=array[0];

for (int i=0;i<arraySize;i++)
{	
  if(array[i]>max)
  {
  max=array[i];
  }
}

return max;
}

double XGetMin(double array[],double arraySize)
{
double min=array[0];

for (int i=0;i<arraySize;i++)
{	
  if(array[i]<min)
  {
  min=array[i];
  }
}

return min;
}

int XGetNumberOrder(double value) 
{
int number;

for (int i=6;i>-6;i--)
{
  if (fabs(value/pow(10,i))>1)
  {
  number=i;
  break;

  //dfddxsx
  //bdjkbdb
  }
}

return number;
}
