namespace IRL_Gui_Debugger.Utils
{
    public class KeyMessageFilter : IMessageFilter
    {
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_KEYUP = 0x0101;
        private bool m_keyDown = false;

        public event EventHandler<CaptureKeyEventArgs>? NavigationKeyPressed;
        public event EventHandler<CaptureKeyEventArgs>? NavigationKeyReleased;

        public bool CaptureKeyEvents { get; set; }

        public bool PreFilterMessage(ref Message m)
        {
            if (CaptureKeyEvents && !m_keyDown && m.Msg == WM_KEYDOWN)
            {
                Keys keyPressed = (Keys)m.WParam;

                if ((keyPressed == Keys.Left) || (keyPressed == Keys.Up) || (keyPressed == Keys.Right) ||
                    (keyPressed == Keys.Down) || (keyPressed == Keys.Space))
                {
                    m_keyDown = true;
                    OnKeyDown(new CaptureKeyEventArgs(keyPressed));

                    return true;
                }
            }

            if (CaptureKeyEvents && m_keyDown && m.Msg == WM_KEYUP)
            {
                Keys keyReleased = (Keys)m.WParam;

                if ((keyReleased == Keys.Left) || (keyReleased == Keys.Up) || (keyReleased == Keys.Right) ||
                    (keyReleased == Keys.Down) || (keyReleased == Keys.Space))
                {
                    m_keyDown = false;
                    OnKeyUp(new CaptureKeyEventArgs(keyReleased));

                    return true;
                }
            }

            return false;
        }

        private void OnKeyDown(CaptureKeyEventArgs e)
        {
            NavigationKeyPressed?.Invoke(this, e);
        }

        private void OnKeyUp(CaptureKeyEventArgs e)
        {
            NavigationKeyReleased?.Invoke(this, e);
        }
    }

    public class CaptureKeyEventArgs : EventArgs
    {
        public Keys Key { get; }

        public CaptureKeyEventArgs(Keys key)
        {
            Key = key;
        }
    }
}
