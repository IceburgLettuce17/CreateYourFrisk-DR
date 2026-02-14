public class CYFException : MoonSharp.Interpreter.ScriptRuntimeException {
    public CYFException(string message) : base(message)
	{
		if (GlobalControls.errorBypass) return;
	}
}