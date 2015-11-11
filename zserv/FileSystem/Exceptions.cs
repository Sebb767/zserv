using System;

namespace zserv.filesytem
{
	/// <summary>
	/// Represents an error while interacting with the filesystem or its configured directives.
	/// </summary>
	public class FileSystemException {}

	/// <summary>
	/// A directive is unknown.
	/// </summary>
	public class UnknownDirectiveException : FileSystemException {}

	/// <summary>
	/// A directive prefix is invalid.
	/// </summary>
	public class InvalidDirectivePrefixException : FileSystemException {}
}

