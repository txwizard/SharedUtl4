/*
    ============================================================================

    Namespace:          WizardWrx

    Class Name:         Utl

    File Name:          Utl.cs

    Synopsis:           This class exposes common general purpose constants and
                        static (Shared in Visual Basic) methods.

    Remarks:            When I figure out how to convert the namespace name into
                        string from which I can construct the absolute resource
                        name, I can put this into a library. Meanwhile, it meets
                        the needs of this assembly.

						The constants bounded by the Frequently Used Constants
						region tags were written into this class to permit this
						library to load without WizardWrx.SharedUtl2.dll, which
						was hampered by its strong name signature. Since these
						constants have since been relocated to a newer library, 
						WizardWrx.DllServices2.dll, which is not so encumbered,
						and is already a dependency of this library for other
						sound reasons, in version 3.0, these constants were
						redefined in terms of their antecedents, all of which
						have either left the WizardWrx.SharedUtl2 namespace or
						fallen into disuse.

    License:            Copyright (C) 2014-2016, David A. Gray. 
						All rights reserved.

                        Redistribution and use in source and binary forms, with
                        or without modification, are permitted provided that the
                        following conditions are met:

                        *   Redistributions of source code must retain the above
                            copyright notice, this list of conditions and the
                            following disclaimer.

                        *   Redistributions in binary form must reproduce the
                            above copyright notice, this list of conditions and
                            the following disclaimer in the documentation and/or
                            other materials provided with the distribution.

                        *   Neither the name of David A. Gray, nor the names of
                            his contributors may be used to endorse or promote
                            products derived from this software without specific
                            prior written permission.

                        THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND
                        CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED
                        WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
                        WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A
                        PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL
                        David A. Gray BE LIABLE FOR ANY DIRECT, INDIRECT,
                        INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
                        (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF
                        SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR
                        PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON
                        ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT
                        LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE)
                        ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN
                        IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

    Created:            Tuesday, 29 July 2014 - Thursday, 31 July 2014

    ----------------------------------------------------------------------------
    Revision History
    ----------------------------------------------------------------------------

    Date       Version Author Description
    ---------- ------- ------ --------------------------------------------------
    2014/07/31 1.0     DAG    Initial implementation.

    2014/07/31 2.7     DAG    Class moved to WizardWrx namespace and SharedUtl4
                              class library. For the moment, WizardWrx.exe
                              shall remain the official test stand. Parts that
                              were intended specifically for use in that program
                              were removed.

    2014/09/21 2.8     DAG    Move DigestToHexDigits into this class from class
                              FileDigest and rename it ByteArrayToHexDigitString
                              to reflect its true calling.

	2016/06/05 3.0     DAG    Break the dependency on WizardWrx.SharedUtl2.dll,
                              correct misspelled words flagged by the spelling
                              checker add-in, and incorporate my three-clause
                              BSD license.
    ============================================================================
*/


using System;

using System.IO;
using System.Reflection;
using System.Text;


namespace WizardWrx
{
    /// <summary>
    /// This static class exposes miscellaneous enumerations, constants, and 
    /// methods that don't really fit into any other class.
    /// </summary>
	[Obsolete ( "The methods and properties defined in this class are redundant. This class is scheduled for elimination in the very near future." )]
	public static class Utl
    {
        #region General Purpose Enumerations
        /// <summary>
        /// This enumeration maps a DataEase Yes/No field.
        /// </summary>
		[Obsolete ( "This enumeration has no business being put into this class, since I used it only once, in a one-off program that was run a handful of times, then shelved." )]
		public enum YesNo
        {
            /// <summary>
            /// The input field is either empty or its value is invalid.
            /// </summary>
            Unspecified,

            /// <summary>
            /// The input field is set to no.
            /// </summary>
            No,

            /// <summary>
            /// The input field is set to yes.
            /// </summary>
            Yes
        }   // YesNo
        
        
        /// <summary>
        /// Evaluate a DataEase GateSystemType choice field.
        /// </summary>
		[Obsolete ( "This enumeration has no business being put into this class, since I used it only once, in a one-off program that was run a handful of times, then shelved." )]
		public enum GateSystemType
        {
            /// <summary>
            /// The input field is either empty or its value is invalid.
            /// </summary>
            Unknown ,

            /// <summary>
            /// The input field is set to DIGI (DigiGate).
            /// </summary>
            DIGI ,

            /// <summary>
            /// The input field is set to PTI, which encompasses both Falcon
            /// 2000 and StorLogix.
            /// </summary>
            PTI ,

            /// <summary>
            /// The input is set to WHAM (Webster Security Systems). So far as I
            /// know, the only location that ever had a WHAM system was the old
            /// Peabody, MA location.
            /// </summary>
            WHAM
        }   // GateSystemType
        #endregion  // General Purpose Enumerations


        #region Frequently Used Constants
        /// <summary>
        /// Always start writing whole blocks at the beginning of the allocated
        /// buffer, which is precisely sized to hold one block.
        /// </summary>
		[Obsolete ( "This constant is identical to WizardWrx.DLLServices2.ListInfo.BEGINNING_OF_BUFFER. Set a reference to WizardWrx.DLLServices2, and use that constant." )]
		public const int BEGINNING_OF_BUFFER = WizardWrx.DLLServices2.ListInfo.BEGINNING_OF_BUFFER;


        /// <summary>
        /// Many methods process labeled delimited text files, and frequently
        /// need to exclude the label row from consideration, for example, when
        /// setting the initial capacity of a collection.
        /// </summary>
		[Obsolete ( "This constant is identical to WizardWrx.MagicNumbers.PLUS_ONE. Set a reference to WizardWrx.DLLServices2, and use that constant." )]
		public const int EXCLUDE_LABEL_ROW = WizardWrx.MagicNumbers.PLUS_ONE;


        /// <summary>
        /// There are several methods that need to know if they are dealing with
        /// the first record of the whole file or of a block.
        /// </summary>
		[Obsolete ( "This constant is identical to WizardWrx.MagicNumbers.PLUS_ONE. Set a reference to WizardWrx.DLLServices2, and use that constant." )]
		public const long FIRST_RECORD = WizardWrx.MagicNumbers.PLUS_ONE;


        /// <summary>
        /// Use this format string, followed by an optional precision 
        /// specification digit, to format a number as a percentage.
        /// 
        /// Be aware that the formatter handles the decimal place shift.
        /// </summary>
		[Obsolete ( "This constant is identical to WizardWrx.NumericFormats.PERCENT. Set a reference to WizardWrx.DLLServices2, and use that constant." )]
		public const string NUMERIC_FORMAT_PERCENTAGE = WizardWrx.NumericFormats.PERCENT;


        /// <summary>
        /// Use this format string to format an integer without any digits
        /// following the (absent) decimal point.
        /// </summary>
		[Obsolete ( "This constant is identical to WizardWrx.DLLServices2.NumericFormats.NUMBER_PER_REG_SETTINGS_0D. Set a reference to WizardWrx.DLLServices2, and use that constant." )]
		public const string NUMERIC_FORMAT_INTEGER_NO_DECIMAL = WizardWrx.NumericFormats.NUMBER_PER_REG_SETTINGS_0D;


        /// <summary>
        /// Arrays subscripting starts at zero. End of discussion.
        /// </summary>
		[Obsolete ( "This constant is identical to WizardWrx.ArrayInfo.ARRAY_FIRST_ELEMENT. Set a reference to WizardWrx.DLLServices2, and use that constant." )]
		public const int ARRAY_FIRST_ELEMENT = WizardWrx.ArrayInfo.ARRAY_FIRST_ELEMENT;


        /// <summary>
        /// Since array subscripting starts at zero, it follows that minus one
        /// is an invalid subscript, which has all sorts of uses, such as
        /// initializing a subscript so that you can tell when it is used for
        /// the first time.
        /// </summary>
		[Obsolete ( "This constant is identical to WizardWrx.ArrayInfo.ARRAY_INVALID_INDEX. Set a reference to WizardWrx.DLLServices2, and use that constant." )]
		public const int ARRAY_INVALID_INDEX = WizardWrx.ArrayInfo.ARRAY_INVALID_INDEX;


        /// <summary>
        /// There are many opportunities when plus one really means "the next
        /// element in the array." Use this constant to document such cases.
        /// </summary>
		[Obsolete ( "This constant is identical to WizardWrx.ArrayInfo.NEXT_INDEX. Set a reference to WizardWrx.DLLServices2, and use that constant." )]
		public const int ARRAY_NEXT_ELEMENT = WizardWrx.ArrayInfo.NEXT_INDEX;


        /// <summary>
        /// Derive the ordinal of an element by adding this constant to its
        /// subscript. Likewise, subtract it from an ordinal to derive the
        /// corresponding subscript.
        /// </summary>
		[Obsolete ( "This constant is identical to WizardWrx.ArrayInfo.ORDINAL_FROM_INDEX. Set a reference to WizardWrx.DLLServices2, and use that constant." )]
		public const int ARRAY_SUBSCRIPT_TO_ORDINAL = WizardWrx.ArrayInfo.ORDINAL_FROM_INDEX;


        /// <summary>
        /// Beginning is an alias for ARRAY_FIRST_ELEMENT.
        /// </summary>
		[Obsolete ( "This constant is identical to WizardWrx.ArrayInfo.ARRAY_FIRST_ELEMENT. Set a reference to WizardWrx.DLLServices2, and use that constant." )]
		public const int BEGINNING = ARRAY_FIRST_ELEMENT;


        /// <summary>
        /// Use this to signify that zero means "none yet."
        /// </summary>
		[Obsolete ( "This constant is identical to WizardWrx.ArrayInfo.ARRAY_IS_EMPTY. Set a reference to WizardWrx.DLLServices2, and use that constant." )]
		public const int NONE_YET = WizardWrx.ArrayInfo.ARRAY_IS_EMPTY;


        /// <summary>
        /// Supply a TAB character, as a one-element array, for splitting.
        /// </summary>
		[Obsolete ( "This object is an array of one SpecialCharacters.TAB_CHAR. Set a reference to WizardWrx.DLLServices2, and use that constant." )]
		public static readonly char [ ] s_achrOneTab = { SpecialCharacters.TAB_CHAR };
        #endregion  // Frequently Used Constants


        #region Public Methods
        /// <summary>
        /// Convert a byte array into a printable hexadecimal representation.
        /// </summary>
        /// <param name="pbytInputData">
        /// Specify the byte array to be formatted. Any byte array will do.
        /// </param>
        /// <returns>
        /// The return value is a string that should contain two characters for
        /// each byte in the array.
        /// </returns>
		[Obsolete ( "This method exactly duplicates WizardWrx.DLLServices2.Util.ByteArrayToHexDigitString. Set a reference to WizardWrx.DLLServices2, and use that method." )]
		public static string ByteArrayToHexDigitString ( byte [ ] pbytInputData )
        {
            StringBuilder sbOutput = new StringBuilder ( pbytInputData.Length );

            //  ----------------------------------------------------------------
            //	Loop through each byte of the hashed data, and format each one
            //	as a hexadecimal string. Although this For loop will never
            //	contain more than one statement, I left the braces to separate
            //	that statement from the third line of the For statement, which I
            //	spread across three lines bacause of its length.
            //  ----------------------------------------------------------------

            for ( int intOffset = ArrayInfo.ARRAY_FIRST_ELEMENT ;
                  intOffset < pbytInputData.Length ;
                  intOffset++ )
            {
                sbOutput.Append ( pbytInputData [ intOffset ].ToString ( NumericFormats.HEXADECIMAL_2 ).ToLowerInvariant ( ) );
            }	//	for ( int intOffset = ArrayInfo.ARRAY_FIRST_ELEMENT ; ...

            return sbOutput.ToString ( );		//	Return the hexadecimal string.
        }   // public static string ByteArrayToHexDigitString


        /// <summary>
        /// Divide the Part by the Whole, and return the quotient expressed and
        /// formatted as a percentage, to the default precision specified in the
        /// current culture.
        /// </summary>
        /// <param name="pintPart">
        /// Part is the integer that represents the amount of work that has been
        /// completed. Both pintPart and pintWhole nus t be expressed in the same
        /// units.
        /// </param>
        /// <param name="pintWhole">
        /// Whole is the integer that represents the total work to be done. Both
        /// pintPart and pintWhole nus t be expressed in the same units.
        /// </param>
        /// <returns>
        /// The return value is a string representation of the two inputs, 
        /// expressed and formatted as a percentage.
        /// </returns>
		[Obsolete ( "This method belongs in DisplayFormats class in WizardWrx.DllServices2.dll, and is scheduled for immediate location thereto, most likely implemented as a generic method." )]
		public static string DisplayPercentage (
            int pintPart ,
            int pintWhole )
        {
            return DisplayPercentage (
                ( Double ) pintPart ,
                ( Double ) pintWhole );
        }   // public static string DisplayPercentage (1 of 6)


        /// <summary>
        /// Divide the Part by the Whole, and return the quotient expressed and
        /// formatted as a percentage, to the default precision specified in the
        /// current culture.
        /// </summary>
        /// <param name="plngPart">
        /// Part is the integer that represents the amount of work that has been
        /// completed. Both pintPart and pintWhole nus t be expressed in the same
        /// units.
        /// </param>
        /// <param name="plngWhole">
        /// Whole is the integer that represents the total work to be done. Both
        /// pintPart and pintWhole nus t be expressed in the same units.
        /// </param>
        /// <returns>
        /// The return value is a string representation of the two inputs, 
        /// expressed and formatted as a percentage.
        /// </returns>
		[Obsolete ( "This method belongs in DisplayFormats class in WizardWrx.DllServices2.dll, and is scheduled for immediate location thereto, most likely implemented as a generic method." )]
		public static string DisplayPercentage (
            long plngPart ,
            long plngWhole )
        {
            return DisplayPercentage (
                ( Double ) plngPart ,
                ( Double ) plngWhole );
        }   // public static string DisplayPercentage (2 of 6)


        /// <summary>
        /// Divide the Part by the Whole, and return the quotient expressed and
        /// formatted as a percentage, to the default precision specified in the
        /// current culture.
        /// </summary>
        /// <param name="pdblPart">
        /// Part is the integer that represents the amount of work that has been
        /// completed. Both pintPart and pintWhole nus t be expressed in the same
        /// units.
        /// </param>
        /// <param name="pdblgWhole">
        /// Whole is the integer that represents the total work to be done. Both
        /// pintPart and pintWhole nus t be expressed in the same units.
        /// </param>
        /// <returns>
        /// The return value is a string representation of the two inputs, 
        /// expressed and formatted as a percentage.
        /// </returns>
		[Obsolete ( "This method belongs in DisplayFormats class in WizardWrx.DllServices2.dll, and is scheduled for immediate location thereto, most likely implemented as a generic method." )]
		public static string DisplayPercentage (
            Double pdblPart ,
            Double pdblgWhole )
        {
            Double dblPercentage = pdblPart / pdblgWhole;
            return dblPercentage.ToString ( NUMERIC_FORMAT_PERCENTAGE );
        }   // public static string DisplayPercentage (3 of 6)


        /// <summary>
        /// Divide the Part by the Whole, and return the quotient expressed and
        /// formatted as a percentage, to the precision specified in the third
        /// (pintPrecision) argument.
        /// </summary>
        /// <param name="pintPart">
        /// Part is the integer that represents the amount of work that has been
        /// completed. Both pintPart and pintWhole nus t be expressed in the same
        /// units.
        /// </param>
        /// <param name="pintWhole">
        /// Whole is the integer that represents the total work to be done. Both
        /// pintPart and pintWhole nus t be expressed in the same units.
        /// </param>
        /// <param name="pintPrecision">
        /// The precision is an integer that specifies the number of places to
        /// show after the decimal point.
        /// </param>
        /// <returns>
        /// The return value is a string representation of the two inputs, 
        /// expressed and formatted as a percentage.
        /// </returns>
		[Obsolete ( "This method belongs in DisplayFormats class in WizardWrx.DllServices2.dll, and is scheduled for immediate location thereto, most likely implemented as a generic method." )]
		public static string DisplayPercentage (
            int pintPart ,
            int pintWhole ,
            int pintPrecision )
        {
            return DisplayPercentage (
                ( Double ) pintPart ,
                ( Double ) pintWhole ,
                pintPrecision );
        }   // public static string DisplayPercentage (4 of 6)


        /// <summary>
        /// Divide the Part by the Whole, and return the quotient expressed and
        /// formatted as a percentage, to the precision specified in the third
        /// (pintPrecision) argument.
        /// </summary>
        /// <param name="plngPart">
        /// Part is the integer that represents the amount of work that has been
        /// completed. Both pintPart and pintWhole nus t be expressed in the same
        /// units.
        /// </param>
        /// <param name="plngWhole">
        /// Whole is the integer that represents the total work to be done. Both
        /// pintPart and pintWhole nus t be expressed in the same units.
        /// </param>
        /// <param name="pintPrecision">
        /// The precision is an integer that specifies the number of places to
        /// show after the decimal point.
        /// </param>
        /// <returns>
        /// The return value is a string representation of the two inputs, 
        /// expressed and formatted as a percentage.
        /// </returns>
		[Obsolete ( "This method belongs in DisplayFormats class in WizardWrx.DllServices2.dll, and is scheduled for immediate location thereto, most likely implemented as a generic method." )]
		public static string DisplayPercentage (
            long plngPart ,
            long plngWhole ,
            int pintPrecision )
        {
            return DisplayPercentage (
                ( Double ) plngPart ,
                ( Double ) plngWhole ,
                pintPrecision );
        }   // public static string DisplayPercentage (5 of 6)


        /// <summary>
        /// Divide the Part by the Whole, and return the quotient expressed and
        /// formatted as a percentage, to the precision specified in the third
        /// (pintPrecision) argument.
        /// </summary>
        /// <param name="pdblPart">
        /// Part is the value that represents the amount of work that has been
        /// completed. Both pintPart and pintWhole nus t be expressed in the same
        /// units.
        /// </param>
        /// <param name="pdblWhole">
        /// Whole is the value that represents the total work to be done. Both
        /// pintPart and pintWhole nus t be expressed in the same units.
        /// </param>
        /// <param name="pintPrecision">
        /// The precision is an integer that specifies the number of places to
        /// show after the decimal point.
        /// </param>
        /// <returns>
        /// The return value is a string representation of the two inputs, 
        /// expressed and formatted as a percentage.
        /// </returns>
		[Obsolete ( "This method belongs in DisplayFormats class in WizardWrx.DllServices2.dll, and is scheduled for immediate location thereto, most likely implemented as a generic method." )]
		public static string DisplayPercentage (
            double pdblPart ,
            double pdblWhole ,
            int pintPrecision )
        {
            Double dblPercentage = pdblPart / pdblWhole;
            return dblPercentage.ToString (
                NUMERIC_FORMAT_PERCENTAGE
                + pintPrecision.ToString (
                    NUMERIC_FORMAT_INTEGER_NO_DECIMAL ) );
        }   // public static string DisplayPercentage (6 of 6)


        /// <summary>
        /// Load the lines of a plain ASCII text file that has been stored with
        /// the assembly as a embedded resource into an array of native strings.
        /// </summary>
        /// <param name="pstrResourceName">
        /// Specify the fully qualified resource name, which is its source file
        /// name appended to the default application namespace.
        /// </param>
        /// <returns>
        /// The return value is an array of Unicode strings, each of which is
        /// the text of a line from the original text file, sans terminator.
        /// </returns>
        /// <see cref="LoadTextFileFromAnyAssembly"/>
        /// <seealso cref="LoadTextFileFromEntryAssembly"/>
        public static string [ ] LoadTextFileFromCallingAssembly (
            string pstrResourceName )
        {
            return LoadTextFileFromAnyAssembly (
                pstrResourceName ,
                Assembly.GetCallingAssembly ( ) );
        }   // public static string [ ] LoadTextFileFromCallingAssembly


        /// <summary>
        /// Load the lines of a plain ASCII text file that has been stored with
        /// the assembly as a embedded resource into an array of native strings.
        /// </summary>
        /// <param name="pstrResourceName">
        /// Specify the fully qualified resource name, which is its source file
        /// name appended to the default application namespace.
        /// </param>
        /// <returns>
        /// The return value is an array of Unicode strings, each of which is
        /// the text of a line from the original text file, sans terminator.
        /// </returns>
        /// <see cref="LoadTextFileFromAnyAssembly"/>
        /// <seealso cref="LoadTextFileFromCallingAssembly"/>
        public static string [ ] LoadTextFileFromEntryAssembly (
            string pstrResourceName )
        {
            return LoadTextFileFromAnyAssembly (
                pstrResourceName ,
                Assembly.GetEntryAssembly ( ) );
        }   // public static string [ ] LoadTextFileFromEntryAssembly


        /// <summary>
        /// Load a text file from any assembly. Since file resources are stored
        /// in binary form, and are read, in binary mode, into a byte array, a
        /// file that contains ASCII text must be converted into native .NET
        /// strings. This routine hides the work required to make that happen.
        /// </summary>
        /// <param name="pstrResourceName">
        /// Specify the fully qualified resource name, which is its source file
        /// name appended to the default application namespace.
        /// </param>
        /// <param name="pasmSource">
        /// Pass in a reference to the Assembly from which you expect to load
        /// the text file. Use any means at your disposal to obtain a reference
        /// from the System.Reflection namespace.
        /// </param>
        /// <returns></returns>
        /// <seealso cref="LoadTextFileFromCallingAssembly"/>
        /// <seealso cref="LoadTextFileFromEntryAssembly"/>
        private static string [ ] LoadTextFileFromAnyAssembly (
            string pstrResourceName ,
            Assembly pasmSource )
        {
            string strInternalName = GetInternalResourceName (
                pstrResourceName ,
                pasmSource );

            if ( strInternalName == null )
                throw new Exception (
                    string.Format (
                        Properties.Resources.ERRMSG_EMBEDDED_RESOURCE_NOT_FOUND ,
                        pstrResourceName ,
                        pasmSource.FullName ) );

            Stream stroTheFile = pasmSource.GetManifestResourceStream ( strInternalName );

            //  ----------------------------------------------------------------
            //  The character count is used several times, always as an integer.
            //  Cast it once, and keep it, since implicit casts create new local
            //  variables.
            //
            //  The integer is immediately put to use, to allocate a byte array,
            //  which must have room for every character in the input file.
            //  ----------------------------------------------------------------

            int intTotalBytesAsInt = ( int ) stroTheFile.Length;
            byte [ ] abytWholeFile = new Byte [ intTotalBytesAsInt ];
            int intBytesRead = stroTheFile.Read (
                abytWholeFile ,                         // Buffer sufficient to hold it.
                BEGINNING_OF_BUFFER ,                   // Read from the beginning of the file.
                intTotalBytesAsInt );                   // Swallow the file whole.

            //  ----------------------------------------------------------------
            //  Though its backing store is a resource embedded in the assembly,
            //  it must be treated like any other stream. Investigating in the
            //  Visual Studio Debugger showed me that it is implemented as an
            //  UnmanagedMemoryStream. That "unmanaged" prefix is a clarion call
            //  that the stream must be cloaed, disposed, and destroyed.
            //  ----------------------------------------------------------------

            stroTheFile.Close ( );
            stroTheFile.Dispose ( );
            stroTheFile = null;

            //  ----------------------------------------------------------------
            //  In the unlikely event that the byte count is short (or long),
            //  the program must croak. Since the three items that we want to
            //  include in the report are stored in local variables, including
            //  the reported file length, we can go ahead and close the stream
            //  before the count of bytes read is evaluated. HOWEVER, you must
            //  USE them, or you get a null reference exception that masks the
            //  real error.
            //  ----------------------------------------------------------------

            if ( intBytesRead != intTotalBytesAsInt )
                throw new InvalidDataException (
                    string.Format (
                        Properties.Resources.ERRMSG_EMBEDDED_RESOURCE_READ_ERROR ,
                        new object [ ]
                        {
                            strInternalName ,
                            intTotalBytesAsInt ,
                            intBytesRead ,
                            Environment.NewLine
                        } ) );

            //  ----------------------------------------------------------------
            //  The file is stored in single-byte ASCII characters. The native 
            //  character set of the Common Language Runtime is Unicode. A new
            //  array of Unicode characters serves as a translation buffer which
            //  is filled a character at a time from the byte array.
            //  ----------------------------------------------------------------

            char [ ] achrWholeFile = new char [ intTotalBytesAsInt ];

            for ( int intCurrentByte = BEGINNING_OF_BUFFER ;
                      intCurrentByte < intTotalBytesAsInt ;
                      intCurrentByte++ )
                achrWholeFile [ intCurrentByte ] = ( char ) abytWholeFile [ intCurrentByte ];

            //  ----------------------------------------------------------------
            //  The character array converts to a Unicode string in one fell
            //  swoop. Since the new string vanishes when StringOfLinesToArray
            //  returns, the constructor call is nested in StringOfLinesToArray,
            //  which splits the lines of text, with their DOS line terminators,
            //  into the required array of strings.
            //
            //  Ideally, the blank line should be removed. However, since the
            //  RemoveEmptyEntries member of the StringSplitOptions enumeration
            //  does it for me, I may as well use it, and save myself the future
            //  agrravation, when I will have probably why it happens.
            //  ----------------------------------------------------------------

            return WizardWrx.TextBlocks.StringOfLinesToArray (
                new string ( achrWholeFile ) ,
                StringSplitOptions.RemoveEmptyEntries );
        }   // private static string [ ] LoadTextFileFromAnyAssembly
        #endregion  // Public Methods


        #region Private Static Methods
        /// <summary>
        /// Use the list of Manifest Resource Names returned by method
        /// GetManifestResourceNames on a specified assembly. Each of several
        /// methods employs a different mechanism to identify the assembly of
        /// interest.
        /// </summary>
        /// <param name="pstrResourceName">
        /// Specify the name of the file from which the embedded resource was
        /// created. Typically, this will be the local name of the file in the
        /// source code tree.
        /// </param>
        /// <param name="pasmSource">
        /// Pass a reference to the Assembly that is supposed to contain the
        /// desired resource.
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value is the internal name of
        /// the requested resource, which is fed to GetManifestResourceStream on
        /// the same assembly, which returns a read-only Stream backed by the
        /// embedded resource. If the specified resource is not found, it
        /// returns null (Nothing in Visual Basic).
        /// </returns>
        private static string GetInternalResourceName (
            string pstrResourceName , 
            Assembly pasmSource )
        {
            foreach ( string strManifestResourceName in pasmSource.GetManifestResourceNames ( ) )
                if ( strManifestResourceName.EndsWith ( pstrResourceName ) )
                    return strManifestResourceName;

            return null;
        }   // private static string GetInternalResourceName
        #endregion  //  Private Static Methods
    }   // public static class
}   // partial namespace WizardWrx