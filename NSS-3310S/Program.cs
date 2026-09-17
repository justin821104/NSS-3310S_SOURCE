using System;
using System.Windows.Forms;

namespace NSS_3310S
{
    static class Program
    {
        /// <summary>
        /// 해당 응용 프로그램의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // 1. UI 스레드 예외 처리
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            // 2. 비동기/일반 스레드 예외 처리 (이게 이벤트 로그에 안 남는 주범일 확률이 높음)
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());
        }

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e) {
            LogException("UI Thread Exception", e.Exception);
        }
        // 비동기/기타 스레드 예외 로그 기록
        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e) {
            LogException("Unhandled Exception", (Exception)e.ExceptionObject);
        }
        // 공통 로그 기록 메서드
        static void LogException(string type, Exception ex) {
            try {
                string logPath = "D:\\CrashLog.txt"; // 저장 경로 (실제 설비용 절대경로 추천)
                string logContent = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{type}]\n" +
                                    $"Message: {ex.Message}\n" +
                                    $"StackTrace: {ex.StackTrace}\n" +
                                    $"{new string('-', 50)}\n";

                // BOM 없는 UTF-8로 즉시 저장
                System.IO.File.AppendAllText(logPath, logContent, new System.Text.UTF8Encoding(false));
            }
            catch {
                // 로그 기록조차 실패할 경우를 대비 (무한 루프 방지)
            }
        }
    }
}
