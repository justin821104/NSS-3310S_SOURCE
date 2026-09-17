using Object;
using System;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

public static class MathHelper
{
    /// <summary>
    /// 각도값을 라디안(rad)으로 리턴
    /// </summary>
    /// <param name="val">각도</param>
    /// 
    /// <returns></returns>
    public static double ToRadians(this double val) { return (System.Math.PI / 180) * val; }

    /// <summary>
    /// 라디안(rad)값을 각도값으로 리턴
    /// </summary>
    /// <param name="val"></param>
    /// <returns></returns>
    public static double ToDegree(this double val){
        double dVal = Math.PI / 180;
        return val / dVal;
    }
}

public class CMATH : DATA_
{
    //public delegate void DeleException(string strErr);
    //public event DeleException deleException = null;

    [DllImport("kernel32.dll")]
    extern static short QueryPerformanceCounter(ref long x);
    [DllImport("kernel32.dll")]
    extern static short QueryPerformanceFrequency(ref long x);

    public double IsNumber(string s){
        if (string.IsNullOrEmpty(s)) return 0;
        try{
            double d = Convert.ToDouble(s);
            return d;
        }
        catch (Exception ex){
            MessageBox.Show(s + ETC.NewLine + ex.Message);
        }
        return 0;
    }

    public void GET_CPU_SPEED(ref long spd){
        long lspd = 0;
        QueryPerformanceFrequency(ref lspd);
        spd = lspd;
    }

    public void GET_CPU_CLOCK(ref long clock){
        long lclock = 0;
        QueryPerformanceCounter(ref lclock);
        clock = lclock;
    }

    /// <summary>
    ///  
    /// </summary>
    /// <param name="cpuSpd"></param>
    /// <param name="sCount"></param>
    /// <param name="eCount"></param>
    /// <returns></returns>
    public double TimeMeasure(double cpuSpd, long sCount, long eCount){
        double lapse;
        lapse = (eCount - sCount) / (cpuSpd / 1000.0);
        return lapse;   // 측정시간
    }

    /// <summary>
    /// 시간분초 리턴
    /// </summary>
    /// <param name="lTime">초값</param>
    /// <returns>시간:분:초 리턴</returns>
    public string IntToTime(long lTime){
        byte bHour = 0;
        while (true){
            if (lTime >= 3600){
                lTime -= 3600;
                bHour += 1;
            }
            else break;
        }
        int hMod = (int)(lTime % 3600);
        int min = hMod / 60;
        int sec = hMod % 60;
        string s = bHour.ToString() + ":" + min.ToString() + ":" + sec.ToString();
        return s;
    }

    /// <summary>
    /// 백분율
    /// </summary>
    /// <param name="vAll">전체 값</param>
    /// <param name="vPiece">일부 값</param>
    /// <returns>전체 값에서 일부값의 퍼센트 리턴</returns>
    public string GetRate(double vAll, double vPiece){
        string s = " (" + string.Format("{0:0.0}", (vPiece / vAll) * 100) + " %)";
        return s;
    }

    /// <summary>
    /// 퍼센트 계산 1
    /// </summary>
    /// <param name="vAll">전체값</param>
    /// <param name="vPercent">퍼센트</param>
    /// <returns>전체값의 n퍼센트 값 리턴</returns>
    public double GetPercent_1(double vAll, double vPercent){
        return (vAll * vPercent) / 100;
    }

    /// <summary>
    /// 퍼센트 계산 2
    /// </summary>
    /// <param name="value"></param>
    /// <param name="percent"></param>
    /// <returns>n값를 n퍼센트 증가 값 리턴</returns>
    public double GetPercent_2(double value, double percent){
        return value * (1 + (percent / 100));
    }

    /// <summary>
    /// 퍼센트 계산 3
    /// </summary>
    /// <param name="value"></param>
    /// <param name="percent"></param>
    /// <returns>n값을 n퍼센트 감소 값 리턴</returns>
    public double GetPercent_3(double value, double percent){
        return value * (1 - (percent / 100));
    }

    /// <summary>
    /// 홀수 짝수 판단 (짝수 = true / 홀수 = false)
    /// </summary>
    /// <param name="iNum">비교 값</param>
    /// <returns></returns>
    public bool IsEven(int iNum){
        if (iNum % 2 == 0) return true;
        return false;
    }

    public double GetMinMaxComparison(double dVal, double dMin, double dMax){
        double rVal = dVal;
        if (rVal < dMin) rVal = dMin;
        if (rVal > dMax) rVal = dMax;
        return rVal;
    }

    /// <summary>
    /// 두값 중 큰값 리턴
    /// </summary>
    /// <param name="dValue1">첫번째 값</param>
    /// <param name="dValue2">두번째 값</param>
    /// <returns></returns>
    public double IsGetMaxValue(double dVal1, double dVal2){
        if (dVal1 > dVal2) return dVal1;
        return dVal2;
    }

    /// <summary>
    /// 두값 중 작은 값 리턴
    /// </summary>
    /// <param name="dVal1"></param>
    /// <param name="dVal2"></param>
    /// <returns></returns>
    public double IsGetMinValue(double dVal1, double dVal2){
        if (dVal1 > dVal2) return dVal2;
        return dVal1;
    }

    /// <summary>
    /// 배열 
    /// </summary>
    /// <param name="dVal"></param>
    /// <returns></returns>
    public int IsGetArrMaxIndex(int[] iVal){
        int iNUM = iVal.Max();
        if (iNUM < 1) return -1;
        for (int i = 0; i < iVal.Length; i++){
            if (iVal[i] == iNUM) return i; //가장 큰수 위로 
        }
        return -1;
    }

    public int IsGetArrMinIndex(int[] iVal){
        int iNUM = iVal.Min();
        if (iNUM > 1) return -1;
        for (int i = 0; i < iVal.Length; i++){
            if (iVal[i] == iNUM) return i; //가장 작은 위로 
        }
        return -1;
    }

    public void IsGetArrMaxMinValue(double[] dVal, ref double dMax, ref double dMin){
        double min = dVal[0];
        double max = 0;
        for (int idx = 0; idx < dVal.Length; idx++){
            //최소값
            if (min > dVal[idx]) min = dVal[idx];
            //최대값
            if (max < dVal[idx]) max = dVal[idx];
        }
        dMax = max;
        dMin = min;
    }

    public void IsGetArrMaxValue(double[] dVal, ref double dMax){
        double max = 0;
        for (int idx = 0; idx < dVal.Length; idx++){
            if (max < dVal[idx]) max = dVal[idx];
        }
        dMax = max;
    }

    public void IsGetArrMinValue(double[] dVal, ref double dMin){
        double min = dVal[0];
        for (int idx = 0; idx < dVal.Length; idx++){
            if (min > dVal[idx]) min = dVal[idx];
        }
        dMin = min;
    }

    /// <summary>
    /// n의 배수 (n의 배수 = true / n의 배수가 아니면 = false)
    /// </summary>
    /// <param name="iMultiple">배수</param>
    /// <param name="iNum">n</param>
    /// <returns></returns>
    public bool Multiple(int iMultiple, int iN){
        if (iMultiple <= 0) return false;
        if (iN % iMultiple == 0) return true;
        return false;
    }

    /// <summary>
    /// 3 by 3 행렬연산
    /// </summary>
    /// <param name="m3X3"></param>
    /// <returns></returns>
    public double GetMatrix3X3(MATRIX3X3 m3X3){
        double dmx = m3X3.m11 * m3X3.m22 * m3X3.m33 -
                     m3X3.m11 * m3X3.m23 * m3X3.m32 -
                     m3X3.m12 * m3X3.m21 * m3X3.m33 +
                     m3X3.m13 * m3X3.m21 * m3X3.m32 +
                     m3X3.m12 * m3X3.m23 * m3X3.m31 -
                     m3X3.m13 * m3X3.m22 * m3X3.m31;
        return dmx;
    }

    /// <summary>
    /// 배열 크기 조정 후 값 복사 
    /// </summary>
    /// <param name="srcInt"></param>
    /// <param name="dstint"></param>
    public void Resize_n_Copy(int[] srcInt, ref int[] dstint){
        Array.Resize<int>(ref dstint, srcInt.Length);
        Array.Copy(srcInt, dstint, srcInt.Length);
    }

    /// <summary>
    /// 배열 안에 값 정보 찾기
    /// </summary>
    /// <param name="dArray">비교 배열</param>
    /// <param name="min">배열 안에서 작은 값 참조 전달</param>
    /// <param name="max">배열 안에서 큰 값 참조 전달</param>
    /// <param name="average">배열 값의 평균 값 참조 전달</param>
    /// <param name="idxMin">배열 안에서 작은 값 인덱스 참조 전달</param>
    /// <param name="idxMax">배열 안에서 큰 값 인덱스 참조 전달</param>
    public void ArrMinMaxVal(double[] dArray, ref double min, ref double max, ref double average, ref int idxMin, ref int idxMax){
        double[] copArr = new double[dArray.Length];
        try{
            Array.Copy(dArray, copArr, dArray.Length);
            min = copArr.Min();
            max = copArr.Max();
            average = copArr.Average();
            idxMin = Array.IndexOf(copArr, copArr.Min());
            idxMax = Array.IndexOf(copArr, copArr.Max());
        }
        catch{
            min = 0;
            max = 0;
        }
    }

    /// <summary>
    /// 배열 범위 안에 정보 찾기
    /// </summary>
    /// <param name="dArray">비교 배열</param>
    /// <param name="begin">시작 인덱스</param>
    /// <param name="end">끝 인덱스</param>
    /// <param name="min">배열 범위 안에서 작은 값 참조 전달</param>
    /// <param name="max">배열 범위 안에서 큰 값 참조 전달</param>
    /// <param name="average">배열 값의 평균 값 찬조 전달</param>
    /// <param name="idxMin">배열 범위 안에  작은 값 인덱스 번호 참조 전달</param>
    /// <param name="idxMax">배열 범위 안에 큰 값 인덱스 번호 참조 전달</param>
    public void ArrMinMaxRangeVal(double[] dArray, int begin, int end, ref double min, ref double max, ref double average, ref int idxMin, ref int idxMax){
        double[] CopArr = new double[end - begin];
        try{
            Array.Copy(dArray, begin, CopArr, 0, end - begin);
            min = CopArr.Min();
            max = CopArr.Max();
            average = CopArr.Average();
            idxMin = Array.IndexOf(CopArr, CopArr.Min());
            idxMax = Array.IndexOf(CopArr, CopArr.Max());
        }
        catch (Exception){
            min = 0;
            max = 0;
        }
    }

    /// <summary>
    /// int 값 power 계산 (n^x)
    /// </summary>
    /// <param name="x"></param>
    /// <param name="pow"></param>
    /// <returns></returns>
    public int IntPow(int x, uint pow){
        int ret = 1;
        while (pow != 0){
            if ((pow & 1) == 1) ret *= x;
            x *= x;
            pow >>= 1;
        }
        return ret;
    }

    /// <summary>
    /// 수직선 두점의 길이
    /// </summary>
    /// <param name="p1">시작 점</param>
    /// <param name="p2">끝 점</param>
    /// <returns>두점의 길이 리턴</returns>
    public double GetVerticalityTwoPointLength(double p1, double p2) { return p2 - p1; }

    /// <summary>
    /// 좌표 평면 두점 사이의 거리 값
    /// </summary>
    /// <param name="p1">좌표 1</param>
    /// <param name="p2">좌표 2</param>
    /// <returns>좌표 평면 두점 사이의 거리 값 리턴</returns>
    public double GetPointLength(dxy p1, dxy p2){
        double dX = p2.x - p1.x;
        double dY = p2.y - p1.y;
        double dLen = Math.Sqrt(Math.Pow(dX, 2) + Math.Pow(dY, 2));
        return dLen;
    } //좌표 평면 두점 사의 거리 √((x1-x2)² + (y1-y2)²)

    /// <summary>
    /// 두 점 사이의 거리 값
    /// </summary>
    /// <param name="ptStart"></param>
    /// <param name="ptEnd"></param>
    /// <returns></returns>
    public double GetDistance(PointF ptStart, PointF ptEnd){
        double dDist;
        dDist = Math.Sqrt(
                            ((ptEnd.X - ptStart.X) * (ptEnd.X - ptStart.X)) +
                            ((ptEnd.Y - ptStart.Y) * (ptEnd.Y - ptStart.Y))
            );
        return dDist;
    }

    /// <summary>
    /// 두 점 사이의 거리 값
    /// </summary>
    /// <param name="ptStart">좌표 1</param>
    /// <param name="ptEnd">좌표 2</param>
    /// <returns></returns>
    public double GetDistance(dxy ptStart, dxy ptEnd){
        double dDist = Math.Sqrt(
                            ((ptEnd.x - ptStart.x) * (ptEnd.x - ptStart.x)) +
                            ((ptEnd.y - ptStart.y) * (ptEnd.y - ptStart.y))
            );
        return dDist;
    }

    /// <summary>
    /// 두 점 사이의 거리 값
    /// </summary>
    /// <param name="dStartX"></param>
    /// <param name="dStartY"></param>
    /// <param name="dEndX"></param>
    /// <param name="dEndY"></param>
    /// <returns></returns>
    public double GetDistance(double dStartX, double dStartY, double dEndX, double dEndY){
        double dDist = Math.Sqrt(
                            ((dEndX - dStartX) * (dEndX - dStartX)) +
                            ((dEndY - dStartY) * (dEndY - dStartY))
            );
        return dDist;
    }

    public double GetDistanceToPoint(dxy a, dxy b){
        //return (double)Math.Sqrt(Math.Pow(a.x - b.x, 2) + Math.Pow(a.y - b.y, 2));
        return (double)Math.Sqrt((a.x - b.x) * (a.x - b.x) + (a.y - b.y) * (a.y - b.y));
    }

    /// <summary>
    /// dExtendedLineLen만큼 Line의 연장 포인트를 구합니다
    /// </summary>
    /// <param name="dExtendX"></param>
    /// <param name="dExtendY"></param>
    /// <param name="dStartX">라인 시작점 X 값</param>
    /// <param name="dStartY">라인 시작점 Y 값</param>
    /// <param name="dEndX">라인 끝점 X 값</param>
    /// <param name="dEndY">라인 끝점 Y 값</param>
    /// <param name="dExtededLineLen">연장선 길이</param>
    /// <param name="bDirectionReverse">+/- 방향 반전</param>
    public void GetExtendedPoint(ref double dExtendX, ref double dExtendY,
        double dStartX, double dStartY, double dEndX, double dEndY, double dExtededLineLen, bool bDirectionReverse = false)
    {
        double dDiffX, dDiffY, dLength;

        dDiffX = dEndX - dStartX;
        dDiffY = dEndY - dStartY;

        if (bDirectionReverse) dExtededLineLen *= -1.0;

        dLength = Math.Sqrt(dDiffX * dDiffX + dDiffY * dDiffY);
        dExtendX = dEndX + (dEndX - dStartX) / dLength * dExtededLineLen;
        dExtendY = dEndY + (dEndY - dStartY) / dLength * dExtededLineLen;

        if (double.IsNaN(dExtendX)) dExtendX = dEndX;
        if (double.IsNaN(dExtendY)) dExtendY = dEndY;
    }

    public double GetDirectionAngle(double x1, double y1, double x2, double y2){
        x2 -= x1;
        y2 -= y1;
        double Angle = Math.Atan2(y2, x2) * 57.3d;
        return Angle;
    }

    /// <summary>
    /// 삼각형의 높이 구하기 (X의 길이와 각도를 알때) 
    /// </summary>
    /// <param name="x"></param>
    /// <param name="deg"></param>
    /// <returns></returns>
    public double GetLineHeight(double x, double deg){
        double r = deg / 180.0;
        r *= Math.PI;

        double dHeight = x * Math.Tan(r);
        return dHeight;
    }

    /// <summary>
    /// 두선의 교차점을 구한다.
    /// </summary>
    /// <param name="xyLine1_Start"></param>
    /// <param name="xyLine1_End"></param>
    /// <param name="xyLine2_Start"></param>
    /// <param name="xyLine2_End"></param>
    /// <param name="xyIntersectionPt"></param>
    /// <returns></returns>
    public bool GetLineIntersectionPoint(dxy xyLine1_Start, dxy xyLine1_End, dxy xyLine2_Start, dxy xyLine2_End, ref dxy xyIntersectionPt){
        double A1 = xyLine1_End.y - xyLine1_Start.y;
        double B1 = xyLine1_End.x - xyLine1_Start.x;
        double C1 = A1 * xyLine1_Start.x + B1 * xyLine1_Start.y;

        double A2 = xyLine2_End.y - xyLine2_Start.y;
        double B2 = xyLine2_End.x - xyLine2_Start.x;
        double C2 = A2 * xyLine2_Start.x + B2 * xyLine2_Start.y;

        double det = A1 * B2 - A2 * B1;

        // Lines are parallel
        if (det == 0.0) return false;
        xyIntersectionPt.x = (B2 * C1 - B1 * C2) / det;
        xyIntersectionPt.y = (A1 * C2 - A2 * C1) / det;
        return true;
    }

    /// <summary>
    /// Find the point of intersection between
    /// the lines p1 --> p2 and p3 --> p4.
    /// </summary>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <param name="p3"></param>
    /// <param name="p4"></param>
    /// <param name="lines_intersect"></param>
    /// <param name="segments_intersect"></param>
    /// <param name="intersection"></param>
    /// <param name="close_p1"></param>
    /// <param name="close_p2"></param>
    public void FindIntersection(
        PointF p1, PointF p2, PointF p3, PointF p4,
        out bool lines_intersect, out bool segments_intersect,
        out PointF intersection,
        out PointF close_p1, out PointF close_p2)
    {
        // Get the segments' parameters.
        float dx12 = p2.X - p1.X;
        float dy12 = p2.Y - p1.Y;
        float dx34 = p4.X - p3.X;
        float dy34 = p4.Y - p3.Y;

        // Solve for t1 and t2
        float denominator = (dy12 * dx34 - dx12 * dy34);

        float t1 = ((p1.X - p3.X) * dy34 + (p3.Y - p1.Y) * dx34) / denominator;
        if (float.IsInfinity(t1)){
            // The lines are parallel (or close enough to it).
            lines_intersect = false;
            segments_intersect = false;
            intersection = new PointF(float.NaN, float.NaN);
            close_p1 = new PointF(float.NaN, float.NaN);
            close_p2 = new PointF(float.NaN, float.NaN);
            return;
        }
        lines_intersect = true;

        float t2 = ((p3.X - p1.X) * dy12 + (p1.Y - p3.Y) * dx12) / -denominator;

        // Find the point of intersection.
        intersection = new PointF(p1.X + dx12 * t1, p1.Y + dy12 * t1);

        // The segments intersect if t1 and t2 are between 0 and 1.
        segments_intersect = ((t1 >= 0) && (t1 <= 1) && (t2 >= 0) && (t2 <= 1));

        // Find the closest points on the segments.
        if (t1 < 0)         t1 = 0;
        else if (t1 > 1)    t1 = 1;

        if (t2 < 0) t2 = 0;
        else if (t2 > 1) t2 = 1;

        close_p1 = new PointF(p1.X + dx12 * t1, p1.Y + dy12 * t1);
        close_p2 = new PointF(p3.X + dx34 * t2, p3.Y + dy34 * t2);
    }

    /// <summary>
    /// Find the point of intersection between
    /// the lines p1 --> p2 and p3 --> p4.
    /// </summary>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <param name="p3"></param>
    /// <param name="p4"></param>
    /// <param name="lines_intersect"></param>
    /// <param name="segments_intersect"></param>
    /// <param name="intersection"></param>
    /// <param name="close_p1"></param>
    /// <param name="close_p2"></param>
    public void FindIntersection(
        dxy p1, dxy p2, dxy p3, dxy p4,
        out bool lines_intersect, out bool segments_intersect,
        out dxy intersection,
        out dxy close_p1, out dxy close_p2)
    {
        // Get the segments' parameters.
        double dx12 = p2.x - p1.x;
        double dy12 = p2.y - p1.y;
        double dx34 = p4.x - p3.x;
        double dy34 = p4.y - p3.y;

        // Solve for t1 and t2
        double denominator = (dy12 * dx34 - dx12 * dy34);

        double t1 =
            ((p1.x - p3.x) * dy34 + (p3.y - p1.y) * dx34)
                / denominator;
        if (double.IsInfinity(t1)){
            // The lines are parallel (or close enough to it).
            lines_intersect = false;
            segments_intersect = false;
            intersection = new dxy(double.NaN, double.NaN);
            close_p1 = new dxy(double.NaN, double.NaN);
            close_p2 = new dxy(double.NaN, double.NaN);
            return;
        }
        lines_intersect = true;

        double t2 =
            ((p3.x - p1.x) * dy12 + (p1.y - p3.y) * dx12)
                / -denominator;

        // Find the point of intersection.
        intersection = new dxy(p1.x + dx12 * t1, p1.y + dy12 * t1);

        // The segments intersect if t1 and t2 are between 0 and 1.
        segments_intersect =
            ((t1 >= 0) && (t1 <= 1) &&
             (t2 >= 0) && (t2 <= 1));

        // Find the closest points on the segments.
        if (t1 < 0) t1 = 0;
        else if (t1 > 1) t1 = 1;

        if (t2 < 0) t2 = 0;
        else if (t2 > 1) t2 = 1;

        close_p1 = new dxy(p1.x + dx12 * t1, p1.y + dy12 * t1);
        close_p2 = new dxy(p3.x + dx34 * t2, p3.y + dy34 * t2);
    }


    /// <summary>
    /// 각도값을 라디안(rad)으로 리턴
    /// </summary>
    /// <param name="val">각도</param>
    /// <returns></returns>
    //public double ToRadians(this double val) { return (System.Math.PI / 180) * val; }

    /// <summary>
    /// 라디안(rad)값을 각도값으로 리턴
    /// </summary>
    /// <param name="val"></param>
    /// <returns></returns>
    //public double ToDegree(/*this*/ double val){
    //    double dVal = (System.Math.PI / 180);
    //    return val / dVal;
    //}

    /// <summary>
    /// 두 점의 각도를 반환
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public double GetAngle(double x, double y){
        if (x == 0.0 || y == 0.0) return 0;
        double dAngle = Math.Atan(y / x);
        dAngle *= 180.0;
        dAngle /= Math.PI;
        return dAngle;
    }

    /// <summary>
    /// 두 점 사이의 각도를 반환
    /// </summary>
    /// <param name="ptStart">시작 점</param>
    /// <param name="ptEnd">끝 점</param>
    /// <returns></returns>
    public double GetAngle(PointF ptStart, PointF ptEnd){
        //double dAngle = 0.0;

        //Math.Atan2(ptEnd.Y - ptStart.Y, ptEnd.X - ptStart.X);
        //dAngle *= 180.0;
        //dAngle /= Math.PI;
        //return dAngle;

        double y = ptEnd.Y - ptStart.Y;
        double x = ptEnd.X - ptStart.X;
        return Math.Atan2(y, x) * (180d / Math.PI);
    }

    /// <summary>
    /// 두 점 사이의 각도를 반환
    /// </summary>
    /// <param name="ptStart">시작 점</param>
    /// <param name="ptEnd">끝 점</param>
    /// <returns></returns>
    public double GetAngle(dxy ptStart, dxy ptEnd){
        double dAngle;

        dAngle = Math.Atan2(ptEnd.y - ptStart.y, ptEnd.x - ptStart.x);
        dAngle *= 180.0;
        dAngle /= Math.PI;
        return dAngle;
    }

    /// <summary>
    /// 두 점 사이의 각도를 반환
    /// </summary>
    /// <param name="dStartX">시작 점 X</param>
    /// <param name="dStartY">시작 점 Y</param>
    /// <param name="dEndX">끝 점 X</param>
    /// <param name="dEndY">끝 점 Y</param>
    /// <returns></returns>
    public double GetAngle(double dStartX, double dStartY, double dEndX, double dEndY){
        double dAngle = Math.Atan2(dEndY - dStartY, dEndX - dStartX);
        dAngle *= 180.0;
        dAngle /= Math.PI;
        return dAngle;
    }

    ///// <summary>
    ///// 두 점 사이의 각도를 반환
    ///// </summary>
    ///// <param name="x1 (시작점)"></param>
    ///// <param name="x2 (시작점)"></param>
    ///// <param name="y1 (끝점)"></param>
    ///// <param name="y2 (끝점)"></param>
    ///// <returns></returns>
    //public double GetAngle(double x1, double x2, double y1, double y2){
    //    double dTheta = 0;
    //    double lx = (x2 - x1);
    //    double ly = (y1 - y2);
    //    if (lx == 0.0 || ly == 0.0) return 0.0;

    //    if (lx > ly) dTheta = ((Math.Atan(ly / lx)) * (180 / Math.PI));
    //    else dTheta = ((Math.Atan(lx / ly)) * (180 / Math.PI));
    //    return dTheta;
    //}

    /// <summary>
    /// 틀어진 Angle로 좌표점을 반환
    /// </summary>
    /// <param name="dRefX">기준 점 X</param>
    /// <param name="dRefY">기준 점 Y</param>
    /// <param name="dAnlge">각도</param>
    /// <param name="dLength">빗변 길이</param>
    /// <returns></returns>
    public dxy GetAnglePos(double dRefX, double dRefY, double dAnlge, double dLength){
        dxy dpAnglePos;
        double dRad = dAnlge.ToRadians();
        dpAnglePos.x = (dLength * Math.Cos(dRad)) + dRefX;
        dpAnglePos.y = (dLength * Math.Sin(dRad)) + dRefY;
        return dpAnglePos;
    }

    /// <summary>
    /// 틀어진 Angle에 대한 변경 된 좌표값을 반환
    /// </summary>
    /// <param name="dRefX">기준 점 X</param>
    /// <param name="dRefY">기준 점 Y</param>
    /// <param name="dAngle">각도</param>
    /// <param name="dPrevX">변경 전 X</param>
    /// <param name="dPrevY">변경 전 Y</param>
    /// <returns></returns>
    public dxy GetWorkingPos(double dRefX, double dRefY, double dAngle, double dPrevX, double dPrevY){
        dxy dpWorkPos;

        double dWidth = dPrevX + dRefX;
        double dHeight = dPrevY + dRefY;

        dpWorkPos.x = (Math.Cos(dAngle.ToRadians()) * dWidth) -
                      (Math.Sin(dAngle.ToRadians()) * dHeight);
        //dWorkX = dWorkX + MainForm->m_stRecipeInfo.Align.dAlignPosX[nStageNo - 1][0] + GetAlignMarkXOffset(nStageNo, true);

        dpWorkPos.y = (Math.Sin(dAngle.ToRadians()) * dWidth) +
                      (Math.Cos(dAngle.ToRadians()) * dHeight);
        //dWorkY = dWorkY + MainForm->m_stRecipeInfo.Align.dAlignPosY[nStageNo - 1][0] + GetAlignMarkYOffset(nStageNo, true);
        return dpWorkPos;
    }

    /// <summary>
    /// 반지름과 길이를 입력하면 해당하는 각도를 반환
    /// </summary>
    /// <param name="dRadius">반지름</param>
    /// <param name="dLength">길이</param>
    /// <returns></returns>
    public double GetCircleAngle(double dRadius, double dLength){
        double dCircumference = (dRadius * 2.0) * Math.PI;
        return (360.0 * dLength) / dCircumference;
    }

    /// <summary>
    /// 원에서 해당하는 각도의 위치를 가져온다.
    /// </summary>
    /// <param name="dCenterX">중심점 X</param>
    /// <param name="dCenterY">중심점 Y</param>
    /// <param name="dRadius">반지름</param>
    /// <param name="dAngle">각도</param>
    /// <param name="dGetX">반환할 위치 값 X</param>
    /// <param name="dGetY">반환할 위치 값 Y</param>
    public void GetPosCircle(double dCenterX, double dCenterY, double dRadius, double dAngle, ref double dGetX, ref double dGetY){
        dAngle = dAngle.ToRadians();
        dGetX = dRadius * Math.Cos(dAngle) + dCenterX;
        dGetY = dRadius * Math.Sin(dAngle) + dCenterY;
    }

    /// <summary>
    /// 호에서 방향성을 확인한다
    /// -1 : CW
    ///  1 : CCW
    ///  0 : SAME
    /// </summary>
    /// <param name="xyCenter"></param>
    /// <param name="xyStart"></param>
    /// <param name="xyEnd"></param>
    /// <returns></returns>
    public int GetArcDirection(dxy xyCenter, dxy xyStart, dxy xyEnd){
        double c = (xyStart.x - xyCenter.x) * (xyEnd.y - xyCenter.y) - (xyStart.y - xyCenter.y) * (xyEnd.x - xyCenter.x);

        if (c < 0) return -1;  // CW
        else if (c > 0) return 1; // CCW
        return 0; // Same
    }


    public double GetConvRotate(double px, double py, double cx, double cy,
                                    ref double rsltX, ref double rsltY, double t)
    {
        double angle = t * Math.PI / 180;
        double iHeight = cx;
        double iWidth = cy;

        double cos_angle = Math.Cos(angle);
        double sin_angle = Math.Sin(angle);
        double x = px;
        double y = py;

        double tx = x - -iWidth;
        double ty = y - iHeight;
        double rx = (tx * cos_angle) - (ty * sin_angle);
        double ry = (ty * cos_angle) + (tx * sin_angle);
        rx += iWidth;
        ry += iHeight;

        rsltX = rx;
        rsltY = ry;
        return 0;
    }

    /// <summary>
    ///  회전축 중심 구하기
    /// </summary>
    /// <param name="sxy"></param>
    /// <param name="exy"></param>
    /// <param name="exy2"></param>
    /// <param name="rad"></param>
    /// <param name="cxy"></param>
    /// <returns></returns>
    public static bool GetcCenterPt(dxy sxy, dxy exy, dxy exy2, double rad, ref dxy cxy){
        double len = Math.Sqrt((exy2.x - exy.x) * (exy2.x - exy.x) + (exy2.y - exy.y) * (exy2.y - exy.y)) / 2.0f;

        if (IsZero(rad / 2.0f) || IsZero(len)){
            cxy.x = 0.0f;
            cxy.y = 0.0f;
            return false;
        }

        double r = len / (2.0f * Math.Tan(rad / 2.0f));
        double xm = exy.x + (exy2.x - exy.x) / 2.0f;
        double ym = exy.y + (exy2.y - exy.y) / 2.0f;

        double cx1 = xm - (exy2.y - exy.y) / len * r;
        double cy1 = ym + (exy2.x - exy.x) / len * r;
        double cx2 = xm + (exy2.y - exy.y) / len * r;
        double cy2 = ym - (exy2.x - exy.x) / len * r;

        double spcp1LenTol = Math.Abs(Math.Sqrt((sxy.x - cx1) * (sxy.x - cx1) + (sxy.y - cy1) * (sxy.y - cy1)) - r);
        double spcp2LenTol = Math.Abs(Math.Sqrt((sxy.x - cx2) * (sxy.x - cx2) + (sxy.y - cy2) * (sxy.y - cy2)) - r);
        if (spcp1LenTol < spcp2LenTol){
            cxy.x = cx1;
            cxy.y = cy1;
        }
        else{
            cxy.x = cx2;
            cxy.y = cy2;
        }
        return true;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="X1"></param>
    /// <param name="X2"></param>
    /// <param name="Result"></param>
    /// <returns></returns>
    public bool GetMidPoint(dxyt X1, dxyt X2, ref dxyt Result){
        Result.x = (X1.x + X2.x) / 2;
        Result.y = (X1.y + X2.y) / 2;
        return true;
    }


    public double GetRPM(double dRPM, double OnePulseResolution, int OneRotationPulse){
        //Vel(RPM) = ((지정 rpm * 1펄스 분해능) / 60초) * 1회전 펄스)
        double dVel = (double.Parse(string.Format("{0:0.0}", ((dRPM * OnePulseResolution) / 60) * OneRotationPulse)));
        return dVel;
    } //rmp -> vel 변경 함수


    public void ToAccUnitSec(double Spd, double Acc, double Dcc, ref double AccSec, ref double DccSec, ref double AccDistance, ref double DccDistance){
        AccSec = Spd / Acc;
        DccSec = Spd / Dcc;
        AccDistance = (Spd / AccSec) / 2;
        DccDistance = (Spd / DccSec) / 2;
    }

    public void ToAccSec(double spd, double AccSec, double DccSec, ref double AccRate, ref double DccRate, ref double AccDistance, ref double DccDistance){
        AccRate = spd / AccSec;
        DccRate = spd / DccSec;
        AccDistance = (spd / AccSec) / 2;
        DccDistance = (spd / DccSec) / 2;
    }

    public void ToAccDistance(double spd, double AccDistance, double DccDistance, ref double AccRate, ref double DccRate, ref double AccSec, ref double DccSec){
        AccSec = 2 / (spd / AccDistance);
        DccSec = 2 / (spd / DccDistance);
        AccRate = spd / AccSec;
        DccRate = spd / DccSec;
    }

    public double GetHorizone2PointAlign(dxy Pos1, dxy Pos2, dxy CPos, ref dxy rPos1, ref dxy rPos2){
        double dWidth;
        double dHeight;
        dxyt Point1 = new dxyt();
        dxyt Point2 = new dxyt();
        dxyt Ref1 = new dxyt();
        dxyt Ref2 = new dxyt();
        double dTH;
        bool isFWD;

        double dZero = 0.00000000001;
        double dTangent;
        double dRadianAngle, dDegreeAngle;
        double dAngle;
        double Cosin;
        double Sin;

        if (Pos1.y < Pos2.y){
            dHeight = Pos2.y - Pos1.y;
            isFWD = true;
        }
        else{
            dHeight = Pos1.y - Pos2.y;
            isFWD = false;
        }
        dWidth = Math.Abs(Pos1.x - Pos2.x);

        if (!IsZero(dHeight) && !IsZero(dWidth))    dTangent = dHeight / dWidth;
        else                                        dTangent = dZero;

        dRadianAngle = Math.Atan(dTangent);
        dDegreeAngle = RadToDeg(dRadianAngle);
        if (isFWD)  dAngle = dDegreeAngle;
        else        dAngle = -dDegreeAngle;


        Ref1.x = Pos1.x - CPos.x;
        Ref1.y = Pos1.y - CPos.y;
        Ref2.x = Pos2.x - CPos.x;
        Ref2.y = Pos2.y - CPos.y;
        dTH = (dAngle * Math.PI / 180) * 1; // (Radian 변환이 필요함.)

        Cosin = Math.Cos(dTH);
        Sin = Math.Sin(dTH);

        Point1.x = (Ref1.x * Cosin) - (Ref1.y * Sin);
        Point1.y = (Ref1.y * Cosin) + (Ref1.x * Sin);
        Point1.t = dTH;

        Point2.x = (Ref2.x * Cosin) - (Ref2.y * Sin);
        Point2.y = (Ref2.y * Cosin) + (Ref2.x * Sin);
        Point2.t = dTH;

        rPos1.x = double.Parse(string.Format("{0:0.000}", Point1.x + CPos.x));
        rPos1.y = double.Parse(string.Format("{0:0.000}", Point1.y + CPos.y));

        rPos2.x = double.Parse(string.Format("{0:0.000}", Point2.x + CPos.x));
        rPos2.y = double.Parse(string.Format("{0:0.000}", Point2.y + CPos.y));


        //Point1.t = (dTH * Math.PI / 180) * iAlignDir; //* iThDir; // (Radian 변환이 필요함.)
        //Cosin = Math.Cos(Point1.t);
        //Sin = Math.Sin(Point1.t);
        //rPos1.x = Math.Round((Point1.x * Cosin) + (Point1.y * Sin), 3);
        //rPos1.y = Math.Round((-Point1.x * Sin) + (Point1.y * Cosin), 3);

        //Point2.t = Point1.t;
        //Cosin = Math.Cos(Point2.t);
        //Sin = Math.Sin(Point2.t);
        //rPos2.x = Math.Round((Point2.x * Cosin) + (Point2.y * Sin), 3);
        //rPos2.y = Math.Round((-Point2.x * Sin) + (Point2.y * Cosin), 3);

        return dAngle;
    }


    public double Get2PointAlign(dxy Pos1, dxy Pos2, dxy CPos, ref dxy rPos1, ref dxy rPos2, bool bAlignDir = false){
        double dHeight;
        double dBaseLine; // 밑변 
        double dTH;
        bool isFWD; //모타 방향
        dxyt Point1 = new dxyt();
        dxyt Point2 = new dxyt();
        double Cosin;
        double Sin;
        int iAlignDir = 1;


        if (Pos1.y < Pos2.y){ // 역회전. ..
            dHeight = Pos2.y - Pos1.y;
            isFWD = false;
        }
        else{
            dHeight = Pos1.y - Pos2.y;
            isFWD = true; //+
        }
        dBaseLine = (Pos1.x - Pos2.x);
        //if (dBaseLine < 0) iAlignDir = -1;
        dTH = Math.Round((Math.Atan(dBaseLine / dHeight) * 180 / Math.PI), 3);
        if (isFWD) dTH *= -1;

        Point1.x = Pos1.x - CPos.x;
        Point1.y = Pos1.y - CPos.y;
        Point1.t = (dTH * Math.PI / 180) * iAlignDir; //* iThDir; // (Radian 변환이 필요함.)
        Cosin = Math.Cos(Point1.t);
        Sin = Math.Sin(Point1.t);

        rPos1.x = Math.Round((Point1.x * Cosin) + (Point1.y * Sin), 3);
        rPos1.y = Math.Round((-Point1.x * Sin) + (Point1.y * Cosin), 3);
        rPos1.x += CPos.x;
        rPos1.y += CPos.y;

        Point2.x = Pos2.x - CPos.x;
        Point2.y = Pos2.y - CPos.y;
        Point2.t = Point1.t;
        Cosin = Math.Cos(Point2.t);
        Sin = Math.Sin(Point2.t);
        rPos2.x = Math.Round((Point2.x * Cosin) + (Point2.y * Sin), 3);
        rPos2.y = Math.Round((-Point2.x * Sin) + (Point2.y * Cosin), 3);
        rPos2.x += CPos.x;
        rPos2.y += CPos.y;
        
        if (bAlignDir) return dTH *= -1;
        return dTH;
    }

    public double GetRotationTrensform(dxy Pos1, dxy Pos2, dxy CenterPos, ref dxy RtnPos1, ref dxy RtnPos2, bool bDIR){
        double dAngle;

        double dHeight; // 높이
        double dBaseLine; // 밑변 
        double dZero = 0.00000000001;
        bool isFWD; //모타 방향

        double dTangent;
        double dRadianAngle, dDegreeAngle;
        dxy CenterDistance1 = new dxy();
        dxy CenterDistance2 = new dxy();
        dxy Point1 = new dxy();
        dxy Point2 = new dxy();
        double dTheta;

        if (Pos1.y < Pos2.y){ // 역회전.
            dHeight = Pos2.y - Pos1.y;
            isFWD = false;
        }
        else{ // 정회전.
            dHeight = Pos1.y - Pos2.y;
            isFWD = true;
        }
        dBaseLine = Math.Abs(Pos1.x - Pos2.x);
        if (!IsZero(dHeight) && !IsZero(dBaseLine)) dTangent = dHeight / dBaseLine;
        else                                        dTangent = dZero;

        dRadianAngle = Math.Atan(dTangent);
        dDegreeAngle = RadToDeg(dRadianAngle);
        if (isFWD)  dAngle = dDegreeAngle;
        else        dAngle = -dDegreeAngle;

        CenterDistance1.x = Pos1.x - CenterPos.x;
        CenterDistance1.y = Pos1.y - CenterPos.y;
        CenterDistance2.x = Pos2.x - CenterPos.x;
        CenterDistance2.y = Pos2.y - CenterPos.y;

        dTheta = dAngle * Math.PI / 180; // (Radian 변환이 필요함.)

        Point1.x = (CenterDistance1.x * Math.Cos(dTheta)) - (CenterDistance1.y * Math.Sin(dTheta));
        Point1.y = (CenterDistance1.y * Math.Cos(dTheta)) + (CenterDistance1.x * Math.Sin(dTheta));

        Point2.x = (CenterDistance2.x * Math.Cos(dTheta)) - (CenterDistance2.y * Math.Sin(dTheta));
        Point2.y = (CenterDistance2.y * Math.Cos(dTheta)) + (CenterDistance2.x * Math.Sin(dTheta));

        RtnPos1.x = double.Parse(string.Format("{0:0.000}", Point1.x + CenterPos.x));
        RtnPos1.y = double.Parse(string.Format("{0:0.000}", Point1.y + CenterPos.y));

        RtnPos2.x = double.Parse(string.Format("{0:0.000}", Point2.x + CenterPos.x));
        RtnPos2.y = double.Parse(string.Format("{0:0.000}", Point2.y + CenterPos.y));

        if (bDIR) return dAngle;
        else return -dAngle;
    }

    //neon ->
    static public double GetAlignment(double px1, double py1, double px2, double py2, double t, double cx, double cy,
                                    ref double rsltX1, ref double rsltY1, ref double rsltX2, ref double rsltY2, ref double rsltT, bool SP2align)
    {
        double height;              // 삼각형의 높이.
        double baseLine;            // 삼각형의 밑변.            
        bool isForwardTurn;  // 회전 방향 (기본은 정방향)
        double ZERO = 0.00000000001;

        double tangent;     //, sine, cosecant, cotangent;
        double radianAngle, degreeAngle;
        double angle;
        double centerDistanceX1, centerDistanceX2, centerDistanceY1, centerDistanceY2;
        double pointX1, pointX2, pointY1, pointY2;
        double theta;

        if (py1 < py2){
            height = py2 - py1; // 높이
            isForwardTurn = false;        // 역회전.
        }
        else{
            height = py1 - py2; // 높이
            isForwardTurn = true;         // 정회전.
        }

        // 밑변  
        baseLine = Math.Abs(px1 - px2);
        if (!IsZero(height) && !IsZero(baseLine))   tangent = height / baseLine;
        else                                        tangent = ZERO;

        radianAngle = Math.Atan(tangent);
        degreeAngle = RadToDeg(radianAngle);

        if (isForwardTurn)  angle = degreeAngle;
        else                angle = -degreeAngle;

        // ***************** 포인트 값 ***************** 
        // **   X = XCOSΘ - YSINΘ                   **
        // **   Y = YCOSΘ + XSINΘ                   **
        // ********************************************* 

        centerDistanceX1 = px1 - cx;
        centerDistanceY1 = py1 - cy;
        centerDistanceX2 = px2 - cx;
        centerDistanceY2 = py2 - cy;

        theta = angle * Math.PI / 180; // (Radian 변환이 필요함.)

        pointX1 = (centerDistanceX1 * Math.Cos(theta)) - (centerDistanceY1 * Math.Sin(theta));
        pointY1 = (centerDistanceY1 * Math.Cos(theta)) + (centerDistanceX1 * Math.Sin(theta));

        pointX2 = (centerDistanceX2 * Math.Cos(theta)) - (centerDistanceY2 * Math.Sin(theta));
        pointY2 = (centerDistanceY2 * Math.Cos(theta)) + (centerDistanceX2 * Math.Sin(theta));

        rsltX1 = double.Parse(string.Format("{0:0.000}", pointX1 + cx));
        rsltX2 = double.Parse(string.Format("{0:0.000}", pointX2 + cx));
        rsltY1 = double.Parse(string.Format("{0:0.000}", pointY1 + cy));
        rsltY2 = double.Parse(string.Format("{0:0.000}", pointY2 + cy));
        if (SP2align){ //SP2 위치 좌표 차이로 인한 
            rsltT = double.Parse(string.Format("{0:0.000}", t + angle));
            return angle;
        }
        rsltT = double.Parse(string.Format("{0:0.000}", t - angle));
        return -angle;

    }

    static private bool IsZero(double value){
        double ZERO = 0.00000000001;
        return (Math.Abs(value) <= ZERO) ? true : false;
    }

    static private double RadToDeg(double radian){
        double degree = radian / Math.PI * 180.0;
        return degree;
    }

    //달팽이 회전 함수
    static public bool W_Err_Position(ref double dXPos, ref double dYPos, int Err_Count, double OffsetX, double OffsetY){
        bool isXPos;
        bool isPluse;
        int nowMatrix;
        int relPoslnMatrix;
        int i;
        double nowOffsetValueX;
        double nowOffsetValueY;

        if (Err_Count <= 0) return false;

        //1. 행렬 구하기 (달팽이 수열 2D Matrix)
        // ex) 0 (O
        i = 1;
        nowMatrix = 0;
        while (i < int.MaxValue){
            if ((Err_Count >= (i * i)) && (Err_Count < (i + 2) * (i + 2))){
                nowMatrix = i + 2;
                break;
            }
            i += 2;
        }

        //2.행렬 상대 위치 구하기
        relPoslnMatrix = Err_Count - ((nowMatrix - 2) * (nowMatrix - 2) - 1);

        //3.행렬 상
        if (relPoslnMatrix == 1){
            isXPos = true;
            isPluse = true;
        }
        else if (relPoslnMatrix <= (nowMatrix - 1) * 1){
            isXPos = false;
            isPluse = true;
        }
        else if (relPoslnMatrix <= (nowMatrix - 1) * 2){
            isXPos = true;
            isPluse = false;
        }
        else if (relPoslnMatrix <= (nowMatrix - 1) * 3){
            isXPos = false;
            isPluse = false;
        }
        else{
            isXPos = true;
            isPluse = true;
        }

        //4.Offset 부호 정의
        if (isPluse == true){
            nowOffsetValueX = OffsetX;
            nowOffsetValueY = OffsetY;
        }
        else{
            nowOffsetValueX = OffsetX * -1;
            nowOffsetValueY = OffsetY * -1;
        }

        ////5. Offset 적용
        //if (isPluse == true)
        //{
        //    nowOffsetValueX = OffsetX;
        //    nowOffsetValueY = OffsetY;
        //}
        //else
        //{
        //    nowOffsetValueX = OffsetX * -1;
        //    nowOffsetValueY = OffsetY * -1;
        //}

        if (isXPos == true){
            dXPos += nowOffsetValueX;
        }
        else{
            dYPos += nowOffsetValueY;
        }

        //string slog = Err_Count.ToString() + "/" + nowMatrix.ToString() + "/" + relPosInmatrix.ToString() + "/" +
        //    nowOffsetValueX.ToString() + "/" + nowOffsetValueX.ToString() + "/" + dXPos.ToString() + "/" + dYPos.ToString();
        //string slog = Err_Count.ToString() + "/" + nowMatrix.ToString() + "/" + relPoslnMatrix.ToString() ;// "/" + nowOffsetValue.ToString() + "/" + dXpos.ToString() + "/" + dYPos.ToString();        
        //System.Diagnostics.Trace.WriteLine(slog);

        return true;
    }
}