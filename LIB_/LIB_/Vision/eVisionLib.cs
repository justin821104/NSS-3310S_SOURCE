using Euresys.Open_eVision_2_12;
using System.Drawing;
using System.Diagnostics;

namespace eVision
{
    public class eVisionImage
    {
        public EImageBW8 imgSrc;
        public EImageBW8 imgProc;

        private ERGBColor mRgbRed;
        private ERGBColor mRgbGreen;

        public enum DRAW_RESULT : int{
            none = 0,
            draw
        };

        public eVisionImage(){
#if _EURESYS
            imgSrc      = new EImageBW8();
            imgProc     = new EImageBW8();

            mRgbRed     = new ERGBColor(255, 0, 0);
            mRgbGreen   = new ERGBColor(0, 255, 0);
#endif
        }

        public void LoadImgFormFile(string strPath){
#if _EURESYS
            imgSrc.Load(strPath);
            imgProc.Load(strPath);
#endif
        }

        public void DrawImage(Graphics g, float fZoomX, float fZoomY){
#if _EURESYS
            if (imgProc.IsVoid) return;
            imgProc.Draw(g, fZoomX, fZoomY);
#endif
        }
    }

    public class eVisionMatcher{
        public delegate void DeleError(string sMSG);
        public event DeleError OnError = null;

        public EMatcher[] eMatcher = new EMatcher[1];
        public EWorldShape eWorldShape;
        public EUnwarpingLut eLookTable;

        //Match 성공여부
        public bool[] bPatternMatch = new bool[1];

        double dCenterX;
        double dCenterY;
        double dCenterOffsetX;
        double dCenterOffsetY;

        public float dPixelStdev;
        public float dMean;

        #region PROPERTY
        public double PIXEL_PER_MM_X{
            get;
            set;
        }

        public double PIXEL_PER_MM_Y{
            get;
            set;
        }

        public double CENTER_X{
            get { return dCenterX; }
            set{
                dCenterX = value;
                dCenterOffsetX = dCenterX - (2448 / 2);
            }
        }

        public double CENTER_Y{
            get { return dCenterY; }
            set{
                dCenterY = value;
                dCenterOffsetY = (2048 / 2) - dCenterY;
            }
        }

        /// <summary>
        /// 좌측 하단 기준으로 화면 센터와 패턴의 센터와의 거리 X pixel 값
        /// </summary>
        public double CENTER_OFFSET_PX_X{
            get { return dCenterOffsetX; }
        }

        /// <summary>
        /// 좌측 하단 기준으로 화면 센터와 패턴의 센터와의 거리 Y pixel 값
        /// </summary>
        public double CENTER_OFFSET_PX_Y{
            get { return dCenterOffsetY; }
        }

        /// <summary>
        /// 좌측 하단 기준으로 화면 센터와 패턴의 센터와의 거리 X mm 값
        /// </summary>
        public double CENTER_OFFSET_MM_X{
            get { return dCenterOffsetX * PIXEL_PER_MM_X; }
        }

        /// <summary>
        /// 좌측 하단 기준으로 화면 센터와 패턴의 센터와의 거리 Y pixel 값
        /// </summary>
        public double CENTER_OFFSET_MM_Y{
            get { return dCenterOffsetY * PIXEL_PER_MM_Y; }
        }
        #endregion

        public eVisionMatcher(){
#if _EURESYS
            for (int nMatchCnt = 0; nMatchCnt < eMatcher.Length; nMatchCnt++){
                eMatcher[nMatchCnt] = new EMatcher();
            }

            eWorldShape = new EWorldShape();
            eLookTable  = new EUnwarpingLut();
#endif
        }

        public void Clear(){
            for (int nPtnCnt = 0; nPtnCnt < bPatternMatch.Length; nPtnCnt++){
                bPatternMatch[nPtnCnt] = false;
            }
        }

        public bool Learn(int nPtnIdx, EROIBW8 PatternRoi){
#if _EURESYS
            try{
                eMatcher[nPtnIdx].LearnPattern(PatternRoi);
            }
            catch (EException ex){
                OnError?.Invoke(ex.ToString());
                //if (OnError != null) OnError(ex.ToString());
                return false;
            }
#endif
            return true;
        }

        public bool Learn(int nPtnIdx, EROIBW8 PatternRoi, float fMaxScale, float fMinScale, float fMaxAngle = 0.0f, float fMinAngle = 0.0f){
#if _EURESYS
            Debug.Assert(fMaxScale <= 10.0f && fMinScale >= 0.01f, "Scale range is 0.01 ~ 10.0");
            Debug.Assert(fMaxScale >= fMinScale, "Scale must be Max >= Min");
            Debug.Assert(fMaxAngle <= 360.0f && fMinAngle >= -360.0f, "Angle range is -360 ~ 360");
#endif

            try{
                eMatcher[nPtnIdx].MaxScale = fMaxScale;
                eMatcher[nPtnIdx].MinScale = fMinScale;
                eMatcher[nPtnIdx].MaxAngle = fMaxAngle;
                eMatcher[nPtnIdx].MinAngle = fMinAngle;

                eMatcher[nPtnIdx].LearnPattern(PatternRoi);
            }
            catch (EException ex){
                OnError?.Invoke(ex.ToString());
                //if (OnError != null) OnError(ex.ToString());
                return false;
            }
            return true;
        }

        public bool LoadPattern(int nPtnIdx, EMatcher Matcher){
#if _EURESYS
            try{
                Matcher.CopyTo(eMatcher[nPtnIdx]);
            }
            catch (EException ex){
                OnError?.Invoke(ex.ToString());
                //if (OnError != null) OnError(ex.ToString());
                return false;
            }
#endif
            return true;
        }

        public bool LoadPattern(int nPtnIdx, string strPath){
#if _EURESYS
            try{
                eMatcher[nPtnIdx].Load(strPath);
            }
            catch (EException ex){
                OnError?.Invoke(ex.ToString());
                //if (OnError != null) OnError(ex.ToString());
                return false;
            }
#endif
            return true;
        }

    }
}