zserv
=====

A lightweight, threaded file server in C# for serving files and directories.

current version: 0.0.1-dev 

**This server is not fully implemented yet.**

Features
---

- **lightweight** The server is aimed to run even on old and low end hardware.
- **dead simple** A config file is optional and its syntax is learned in less than a minute.
- **zip download** What is every simple file server missing? The option to download whole directories! zserv can serve files as well as directories as zips.

Installation
---

_yet to come_

Configuration
---

zserv uses a directive-based configuration system. You can either specify them globally via the _-o_ command line option or with a config file (specify it via _-c file_). A directive usually looks like this:

```
<path>	[prefix]<option>,[prefix]<option>
/	nozip
/mydata	!priv
/store	nodot,+nosym
```

In this example, zip downloading is disabled for ``/`` and ``/mydata`` is not accesible. ``/store`` will forbid all folders starting with a dot and symlinks in it and all of its subdirectories will be ignored. Note that wildcars `*` are possible at the end of a string, but every directory specification _must_ start with a `/` (relative paths to the above one may be implemented at a later time).

Valid prefixes are:
- ``[none]`` The directive will be valid for the specified directory.
- ``+`` Set directive for this and all of its subdirectories.
- ``-`` Unset directive for this directory and all of its subdirectories. Can't overwrite ``!``.
- ``!`` Enforce this directive. Same as ``+``, but this cannot be overwritten by ``-``. 

Valid directives are:
- ``nozip`` disable zip download.
- ``nozipi`` if a parent directory is downloaded, exclude this directory from the archive. _The directory may still be downloaded as zip._
- ``priv`` do not allow access to this directory. Will also exclude it from zip downloads of parent directories.
- ``nodot`` hide (_not forbid_) all directories starting with a dot.
- ~~``nosym`` don't follow symlinks.~~ Implementation delayed.

Planned features
---
- permission system
- logging 

