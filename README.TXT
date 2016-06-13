                               SharedUtl4 ReadMe

================================================================================
2016-06-10 01:13:49
================================================================================

The purpose of this group of related class libraries is to encapsulate designs
that I have devised to simplify production of reports from programs that that
target any version of the Microsoft .NET Framework, from version 2.0 up.

Where appropriate, these classes define public constants, but their focus is on
encapsulating methods that revolve in one way or another around reporting.

The following table summarizes the focus of each of the three libraries.

Library Name                     Abstract
-------------------------------- -----------------------------------------------
WizardWrx.ASCIIInfo.dll          This is a small single-purpose library, which
                                 encapsulates a table containing entries for all
                                 256 ASCII characters. Properties of its single
                                 class expose text that renders many of them
                                 printable. Others return their numerical values
                                 as integers, which can be rendered as either
                                 decimal or hexadecimal, as needed.

WizardWrx.FormatStringEngine.dll The classes in this library facilitate parsing
                                 and constructing composite format items for use
                                 with the various WriteLine and string.Format
                                 methods. They were created to simplify creation
                                 of format items that have their widths set at
                                 run time, to permit columns of text and numbers
                                 to be aligned vertically, without knowing their
                                 sizes in advance.

WizardWrx.SharedUtl4.dll         This library is divided into two main areas,
                                 with a subsidiary group of classes for managing
                                 lists that exist largely as helpers for the
                                 reporting classes, around which the library was
                                 created. A secondary group that went into this
                                 library, for lack of a better home, is a full
                                 set of message digest wrappers that greatly
                                 simplify the work required to create MD5 and
                                 SHAx (where x = 1, 256, 384, or 512 bits)
                                 message digests (hashes) of files and strings.

The following table briefly describes the classes in a bit more detail. Classes
are grouped alphabetically by library.

Class Name                    Namespace (Full Name)  Abstract
----------------------------- ---------------------- ------------------------------------
ASCII_Character_Display_Table WizardWrx                    This class exposes a
                                                           GetTheSingleInstance
                                                           method and a property,
                                                           AllASCIICharacters an
                                                           ASCIICharacterDisplayInfo
                                                           array), and that's it.

ASCIICharacterDisplayInfo     WizardWrx                    ASCII_Character_Display_Table
                                                           property AllASCIICharacters
                                                           is an array of 256 of these
                                                           objects, each representing
                                                           an ASCII character. The
                                                           properties include decimal
                                                           and hexadecimal formats of
                                                           its numeric code, its value
                                                           as a raw character, and a
                                                           display literal to stand in
                                                           its place if its printed
                                                           representation is deemed
                                                           unusable. Use this table as
                                                           a reference and as a source
                                                           from which to get suitable
                                                           print representations of the
                                                           control characters and other
                                                           special characters.

FormatItem                    WizardWrx.FormatStringEngine This is an infrastructure
                                                           class; instances represent
                                                           a FormatItem found in the
                                                           FormatString associated with
                                                           the FormatStringParser that
                                                           owns it.

                                                           The static methods of this class
                                                           are public, and are very handy for
                                                           creating compact summary reports.

                                                           Use them to construct composite
                                                           format items that have their
                                                           characteristics, especially their
                                                           widths, established at runtime.
                                                           Setting the width dynamically
                                                           permits summary totals and their
                                                           labels, all of which may have
                                                           widely varying widths that are
                                                           unknown in advance to be set in
                                                           such a way that a set of numeric
                                                           values and their labels can be
                                                           printed with labels on the left,
                                                           and a minimum amount of white
                                                           space between the longest label
                                                           and the numbers.

FormatItemsCollection         WizardWrx.FormatStringEngine This is an infrastructure class;
                                                           FormatStringParser instances use
                                                           instances of it to organize data
                                                           about the format items in the
                                                           format control string they are
                                                           parsing.

FormatStringError             WizardWrx.FormatStringEngine This is an infrastructure class.
                                                           FormatStringParser instances cache
                                                           errors found in a format control
                                                           string in this collection, so that
                                                           all can be reported at the end.

FormatStringParser            WizardWrx.FormatStringEngine Use this class to parse format
                                                           control strings that you intend to
                                                           use with string.Format or one of the
                                                           Write or WriteLine methods of a
                                                           stream object, such as a Console or
                                                           TextWriter. Properties and methods
                                                           report on its attributes, such as
                                                           its FormaItems, and errors flagged
                                                           by the parser.

DigestFile                    WizardWrx.Cryptography       Create message digests of the contents
                                                           of files of all kinds and sizes. The
                                                           following algorithms are supported.

                                                           ------------------------------------------------------
                                                           Algorithm   Strength    Hexadecimal String  Use This
                                                           Name        in Bits     Length in Bytes     Method
                                                           ---------   --------    ------------------  ----------
                                                           MD5         128          32                 MD5Hash *
                                                           SHA-1       160          40                 SHA1Hash *
                                                           SHA-256     256          64                 SHA256Hash
                                                           SHA-384     384          96                 SHA384Hash
                                                           SHA-512     512         128                 SHA512Hash
                                                           ------------------------------------------------------

                                                           *   MD5 and SHA-1 are classified as broken and unsafe.
                                                               Both are retained only for backward compatibility.
                                                               References in code will cause the C# compiler to
                                                               emit warning CS0618, and other compilers will emit
                                                               similar warnings.

DigestString                  WizardWrx.Cryptography       Create message digests of the contents
                                                           of Unicode strings. This class supports
                                                           the same algorithms as does DigestFile,
                                                           whth the same caveats.

ErrorMessage                  WizardWrx                    This displays the error messages that
                                                           accompany exit codes, with optional
                                                           substitution tokens read from parameter
                                                           arrays.

ErrorMessagesCollection       WizardWrx                    This class is a specialized derivative
                                                           of ReportDetails; its specialty is
                                                           managing error messages that correspond
                                                           to exit codes, with optional
                                                           substitution tokens read from parameter
                                                           arrays.

ListHelpers                   WizardWrx                    This class exposes methods for merging
                                                           sorted lists of items, and to simplify
                                                           working with the values returned through
                                                           the IComparable interface.

ReportDetail                  WizardWrx                    Instances of this class are generic
                                                           report details, with labels and formats
                                                           for printing them.

ReportDetails                 WizardWrx                    This class holds the details for a report.
                                                           Static object locks are used throughout to
                                                           ensure synchronized access.

ReportHelpers                 WizardWrx                    This static class exposes methods to help
                                                           prepare strings for use on reports.

StringThings                  WizardWrx                    Provide static methods to perform frequently
                                                           required tasks that involve parsing or
                                                           modifying strings such as command line
                                                           arguments coming in and messages going out.

Utl                           WizardWrx                    This class is deprecated, but it must remain
                                                           for the moment, pending a more throrough review
                                                           of references to its constants and methods.

--------------------------
ToDo List
--------------------------

1) ASCIICharacterDisplayInfo: Add a string property to store the XHTML entitties
   for the characters that need one, and populate a new node in the XML table.

2) FormatItem: Devise a more straightforward way to apply the static methods to
   format control strings. Daily use over the last two years has convinced me
   that they are too cumbersome.

3) FormatItem: Implement a FormatItem with an alignment of Centered. This will
   require a custom IFormttable interface implementation, which I recently had
   occasion to do for the newest elements of WizardWrx.BitMath.dll.

4) FormatStringParser: Construct a tool around this class, similar to the long
   established FormatUtility, which focuses on individual format items.

5) Utl: This class is redundant, and will be removed shortly, since all of its
   functionality is duplicated by classes in WizardWrx.DllServices2.dll.

--------------------------
Compatibility Counts!
--------------------------

To maximize compatibility with client code, the library targets version 2.0 of
the Microsoft .NET Framework, enabling it to support projects that target that
version, or any later version, of the framework. Since its implementation needs
only core features of the Base Class Library, I have yet to discover an issue in
using it with any of the newer frameworks.

The class belongs to the WizardWrx namespace, which I created to organize the
helper libraries that I use in virtually every production assembly, regardless
of what framework version is its target, and whether its surface is a Windows
console, the Windows desktop, or the ASP.NET Web server. To date, I have used
classes and methods in these libraries in all three environments. The dedicated
namespace pretty much eliminates name collisions as an issue; at the very least,
a name can be qualified to disambiguate it.

The next several sections cover special considerations of which you must be
aware if you incorporate this package into your code as is or if you want to
modify it.


--------------------------
NuGet Packages
--------------------------

The EnhancedIOException class uses a method exposed by the Newtonsoft.Json class
library. To that end, the repository includes Newtonsoft.Json.8.0.3, and the
build script is configured to pull the latest version from the NuGet repository.

--------------------------
Unmanaged Native Code DLLs
--------------------------

There are a number of native code dynamic link libraries in the output
directories. Only two, P6CUtilLib1.dll and WWConAid.dll, are directly invoked.
Though the routines in those two libraries that are used, via Platform/Invoke,
use a handful of routines from the other libraries, most are there to satisfy
the loader, which expects to find every DLL mentioned in the Import Address
Table, regardless of whether that routine is actually invoked. After all, the
loader has no way to know that you intend to use just one of possibly dozens or
hundreds of exported routines.

All of these DLLs are my own work, and were created using the Microsoft Visual
Studio compilers and tools. The newer libraries were built with Visual Studio
2013, and link to MSVCR120.dll. The rest were built with Visual Studio 6, and
link to MSVCRT.dll. Although I made significant use of the CRT libraries, even
the routines that write to streams, no resources created by one version of the
CRT DLL is ever processed by the other, and I have run these routines in dozens
of applications, many of which run daily, for over two years, without incident.

--------------------------
Required External Tools
--------------------------

The pre and post build tesks and the test scripts found in the /scripts
directory use a number of tools that I have developed over many years. Since
they live in a directory that is on the PATH list on my machine, they are "just
there" when I need them, and I seldom give them a second thought. To simplify
matters for anybody who wants to run the test scripts or build the project, they
are in DAGDevTOOLS.ZIP, which can be extracted into any directory that happens
to be on your PATH list. None of them requires installation, none of the DLLs is
registered for COM, and none of them or their DLLs use the Windows Registry.

A few use MSVCR120.dll, which is not included, but you probably have it if you
have a compatible version of Microsoft Visual Studio. The rest use MSVCRT.DLL,
which ships with Microsoft Windows.

Rather than deposit a copy of the tool kit in each repository, and incur a very
significant maintenance burden, they have their own repository, at the following
URL.

     https://github.com//txwizard/DAGDevTOOLS

Whereas this repository has a three-clause BSD license, the tool kit has a
freeware license. Although I anticipate eventually putting most, if not all, of
the binary code in the tool kit into open source repositories, due to the way
their source code is organized, making usable repositories is nontrivial, and
must wait for another day. Meanwhile, the shell scripts shall remain completely
free to use and adapt.

--------------------------
Internal Documentation
--------------------------

The source code includes comprehenisve technical documentation, including XML to
generate IntelliSense help, from which the build engine generates XML documents,
which are included herein. Argument names follow Hungarian notation, to make the
type immediately evident in most cases. A lower case "p" precedes a type prefix,
to differentiate arguments from local variables, followed by a lower case "a" to
designate arguments that are arrays. Object variables have an initial underscore
and static variables begin with "s_"; this naming scheme makes variable scope
crystal clear.

The classes are thoroughly cross referenced, and many properties and methods
have working links to relevant MSDN pages.
================================================================================
