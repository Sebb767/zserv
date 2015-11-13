using System;

namespace zserv.filesytem
{
	/// <summary>
	/// Represents an error while interacting with the filesystem or its configured directives.
	/// </summary>
	public class FileSystemException : Exception 
	{
		public FileSystemException (string message) :	base(message) {}
	}

	/// <summary>
	/// A directive is unknown.
	/// </summary>
	public class UnknownDirectiveException : FileSystemException 
	{
		public UnknownDirectiveException (string message) :	base(message) {}
	}

	/// <summary>
	/// A directive prefix is invalid.
	/// </summary>
	public class InvalidDirectivePrefixException : FileSystemException 
	{
		public InvalidDirectivePrefixException (string message) :	base(message) {}
	}

	/// <summary>
	/// A directive is already set for a directory.
	/// </summary>
	public class DirectiveAlreadySetException : FileSystemException 
	{
		public readonly string name;

		public DirectiveAlreadySetException (string name) 
			:	base("The directive %s is already set.".incorporate(name)) 
		{
			this.name = name;
		}
	}

	/// <summary>
	/// Malformed line in dir config.
	/// </summary>
	public class MalformedDirectiveException : FileSystemException 
	{
		public MalformedDirectiveException (string line, string details) 
			: base("The config line `%s` is malformed: %s.".incorporate(line, details)) {}
	}
}

