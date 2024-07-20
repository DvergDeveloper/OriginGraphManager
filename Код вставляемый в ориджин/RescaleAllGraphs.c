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

#include <stdio.h>
#include <string.h>

#include <math.h>

////////////////////////////////////////////////////////////////////////////////////

//#pragma labtalk(0) // to disable OC functions for LT calling.

////////////////////////////////////////////////////////////////////////////////////
// Include your own header files here.


////////////////////////////////////////////////////////////////////////////////////
// Start your functions here.

int RescaleGraphs()
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
Scale graphScale(grLayer.Y);
Scale XgraphScale(grLayer.X);

//DataPlots===========================
double localMin = NaN;
double localMax = NaN;

double minCondidate;
double maxCondidate;

foreach(DataPlot dplot in grLayer.DataPlots)
{

lastIndex = GetLastIndex(dplot);
startIndex = GetIndexOfStart(dplot,lastIndex, XgraphScale.From);
endIndex = GetIndexOfEnd(dplot,lastIndex, XgraphScale.To);

minCondidate=GetMinLocalY(dplot,startIndex,endIndex);
maxCondidate=GetMaxLocalY(dplot,startIndex,endIndex);

if (localMin==NaN)
{
localMin=minCondidate;
}
else if(localMin>minCondidate)
{
localMin=minCondidate;
}

if (localMax==NaN)
{
localMax=maxCondidate;
}
else if(localMax<maxCondidate)
{
localMax=maxCondidate;
}

}
//====================================

double someArray[2];
someArray[0]=localMin;
someArray[1]=localMax;
double someArraySize =2;

// Calculate borders and ticks===========================
double toOne = GetToOneCoeff(someArray,someArraySize);

double buttomBorder = GetButtomBorder(someArray,someArraySize,toOne);
double upBorder = GetUpBorder(someArray,someArraySize,toOne);

double ticks[4];
ticks[0]=0.1*toOne;
ticks[1]=0.5*toOne;
ticks[2]=1*toOne;
ticks[3]=5*toOne;
   
double ticksCount[4];
ticksCount[0]=(upBorder-buttomBorder)/ticks[0];
ticksCount[1]=(upBorder-buttomBorder)/ticks[1];
ticksCount[2]=(upBorder-buttomBorder)/ticks[2];
ticksCount[3]=(upBorder-buttomBorder)/ticks[3];
int minorTicksCount[]={10,5,10,5};

double optTick;
int optMinorTicksCount;

for (int i=0;i<4;i++)
{
  if (ticksCount[i] < 8 && 1.5 < ticksCount[i])
  {
  optTick=ticks[i];
  optMinorTicksCount=minorTicksCount[i];
  break;
  }
}

buttomBorder = ReduceBorder( buttomBorder, optTick, -1,  someArray[0],  someArray[1]);
upBorder     = ReduceBorder( upBorder,     optTick,  1,  someArray[0],  someArray[1]);

buttomBorder = ExtendBorder( buttomBorder, optTick, -1, toOne, optMinorTicksCount);
upBorder     = ExtendBorder( upBorder,     optTick,  1, toOne, optMinorTicksCount);

buttomBorder = PushLineFromBorder( buttomBorder, optTick, -1,  someArray[0],  someArray[1]);
upBorder     = PushLineFromBorder( upBorder,     optTick,  1,  someArray[0],  someArray[1]);


// IA DOBAVIL UDOLI ESLI CHTO
ticksCount[0]=(upBorder-buttomBorder)/ticks[0];
ticksCount[1]=(upBorder-buttomBorder)/ticks[1];
ticksCount[2]=(upBorder-buttomBorder)/ticks[2];
ticksCount[3]=(upBorder-buttomBorder)/ticks[3];

for (int J=0;J<4;J++)
{
  if (8 > ticksCount[J] && 2.1 < ticksCount[J])
  {
  optTick=ticks[J];
  optMinorTicksCount=minorTicksCount[J];
  break;
  }
}




//========================================================================================


// kastil if small value or ConstantValue
if (upBorder<0.3 && buttomBorder>-0.3)
{
buttomBorder=-0.5;
upBorder=0.5;
optTick=0.5;
optMinorTicksCount=3;
}
else if (fabs(upBorder - buttomBorder)<0.1)
{
buttomBorder=floor(buttomBorder/toOne)*toOne;
upBorder=ceil(upBorder/toOne)*toOne;

optTick=0.5*toOne;
optMinorTicksCount=3; 
}
	
graphScale.From=buttomBorder;
graphScale.To=upBorder;
graphScale.Inc=optTick;

if (optMinorTicksCount>7)
{
grLayer.LT_execute("axis -ps Y M 1");
}
else
{
grLayer.LT_execute("axis -ps Y M 4");
}

grLayer.LT_execute("doc -uw");
}

}

}

return 1;
}

// DataPlot methods====================================

int GetLastIndex(DataPlot plot)
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

int GetIndexOfStart(DataPlot plot,int lastIndex,double from)
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

int GetIndexOfEnd(DataPlot plot,int lastIndex,double to)
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

double GetMaxLocalY(DataPlot plot,int from,int to)
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

double GetMinLocalY(DataPlot plot,int from,int to)
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

double GetToOneCoeff(double array[],double sizeArray)
{
return (double)pow(10,GetNumberOrder(GetMaxAbs(array,sizeArray)));
}

double GetButtomBorder(double array[],double sizeArray, double toOne)
{
return ceil(GetMin(array,sizeArray)/toOne-1.01)*toOne;
}

double GetUpBorder(double array[],double sizeArray, double toOne)
{
return floor(GetMax(array,sizeArray)/toOne+1.01)*toOne;
}



double GetTickCase(double ButtomBorder,double UpBorder,double toOne)
{
return (UpBorder-ButtomBorder)/toOne;
}

double GetTick(double toOne,double tickCase)
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

double PushLineFromBorder(double border,double tick,double direction, double min, double max)
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

double ReduceBorder(double border,double tick,double direction, double min, double max)
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


double ExtendBorder(double border,double tick,double direction, double toOne,int minorTicksCount)
{
double nextBorder = border;

for(int i=0;i<minorTicksCount;i++)
{
  if (fabs(mod(nextBorder,tick,toOne))<pow(10,-10)*tick/minorTicksCount)
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

int mod(double num1,double num2, double toOne)
{
int intNum1=(int)round((num1/toOne*10),0);
int intNum2=(int)round((num2/toOne*10),0);

return intNum1%intNum2;
}


double GetMaxAbs(double array[],double arraySize)
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

double GetMax(double array[],double arraySize)
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

double GetMin(double array[],double arraySize)
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

int GetNumberOrder(double value) 
{
int number;

for (int i=6;i>-6;i--)
{
  if (fabs(value/pow(10,i))>1)
  {
  number=i;
  break;
  }
}

return number;
}
