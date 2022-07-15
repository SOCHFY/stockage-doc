namespace MessageTypes
{
	using System;
	using System.Collections.Generic;
	using System.Text;
	using global::Avro;
	using global::Avro.Specific;
	
	public enum LogLevel
	{
		None,
		Verbose,
		Info,
		Warning,
		Error,
	}
}