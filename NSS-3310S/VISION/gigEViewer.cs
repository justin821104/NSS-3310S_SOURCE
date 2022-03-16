using System;
using System.Drawing;
using System.Windows.Forms;


namespace NSS_3310S.VISION
{
    [Flags]
    public enum DRAW_MODE
    {
        // 0x00, 0x01,  0x02,  0x04,  0x08,  0x10,  0x20,  0x40, 0x100, 0x200, 0x400, 0x800

        NORMAL = 0x00,
        CENTER_LINE = 0x01,  // CenterLine Draw
        ROI = 0x02,          // ROI
        CAL_GRID = 0x04,     // CAL_CHART_GRID
        MATCH_RESULT = 0x08, // Match
        SEARCH_OBJ = 0x10,   // Object
        SEARCH_OBJ2 = 0x20,
        CAL_GAUGE = 0x40,    // Scanner Calibration
        INSP_GAUGE = 0x80,   // Inspection 
        INSP_AREA = 0x100,   // Insp Area
        AREA_RECT = 0x200,    // VCR, CALIBRATION MARK ROI
        PROJECTION_SEARCH = 0x400,    // Peeling detect area
        SEARCH_AREA = 0x800,    // Peeling detect area
        VIRTUAL_ALIGN_KEY = 0x1000,   // Object
        INSP_GAUGE_ARC = 0x2000,   // Inspection Arc 
        MANUAL_ALIGN_MATCH = 0x4000,   // Inspection Arc 
        ALL = 0xFF,
    }

    [Flags]
    public enum INSP_VIEW_MODE
    {
        // 0x00, 0x01,  0x02,  0x04,  0x08
        NORMAL = 0x00,
        ACTUAL_POINTS = 0x01,
        SAMPLING_POINTS = 0x02,
        EDGE_CORNER = 0x04,
        CHAMFER_CUT = 0x08,
        RESIDUE_CHECK = 0x10,
        CHAMFER_CUT_ARC = 0x20,
        ALL = 0xFF,
    }

    [Flags]
    public enum CAL_VIEW_MODE
    {
        // 0x00, 0x01,  0x02,  0x04,  0x08
        NORMAL = 0x00,
        CIRCLE_SEARCH = 0x01,
        CROSS_LINE_SEARCH = 0x02,
        ALL = 0xFF,
    }

    public class gigEViewer : Panel, IDisposable
    {
        bool alive = false;
        ContextMenuStrip muViewer = new ContextMenuStrip();
        PictureBox pbViewer;

        Size sizeOrg, sizePrev;
        float ratioUcWidth = 1.0f;
        float ratioUcHeight = 1.0f;

        float fZoomX = 1.0f;
        float fZoomY = 1.0f;

        int[] nROIOrgX = new int[2];
        int[] nROIOrgY = new int[2];
        int[] nROIWidth = new int[2];
        int[] nROIHeight = new int[2];

        float ratioScreen = 1.0f;
    }
}
