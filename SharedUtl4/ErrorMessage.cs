/*
    ============================================================================

    Namespace:          WizardWrx

    Class Name:         ErrorMessage

    File Name:          ErrorMessage.cs

    Synopsis:           This derivative of WizardWrx.ReportDetail specializes in
                        storing and displaying an error message that accompanies
                        an exit code.

    Remarks:            Since it is inappropriate for displaying error messages,
                        this class overrides the default format string defined
                        as a static property of the base class, while retaining
                        the mechanism for overriding it.

                        Of the 17 constructors defined by the base classe, only
                        four, the default and three others, are defined. In all
                        three cases, the base class does all the work; the two
                        overloads that have arguments just call constructors
                        in the base class, passing the arguments in the correct
                        order to yield an object that has its message stored in
                        the label field and its exit code value stored in the
                        value field.

                        On the surface, you might think that storing the exit
                        code is unnecessary, since it can be inferred from the
                        item's index in the collection. However, since it may be
                        included in the message, and the object cannot determine
                        its position in the collection, it must be stored.

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

    Created:            Tuesday, 22 July 2014 - Thursday, 24 July 2014

    ----------------------------------------------------------------------------
    Revision History
    ----------------------------------------------------------------------------

    Date       Version Author Description
    ---------- ------- ------ --------------------------------------------------
    2014/07/24 1.0     DAG    Initial implementation.
    
	2014/07/31 2.7     DAG    Class moved to WizardWrx namespace and SharedUtl4
                              class library. For the moment, LogBasePruner.exe
                              shall remain the official test stand.

	2014/09/21 2.8     DAG    Supply missing XML documentation.

    2016/06/05 3.0     DAG    Break the dependency on WizardWrx.SharedUtl2.dll,
                              correct misspelled words flagged by the spelling
                              checker add-in, and incorporate my three-clause
                              BSD license.
    ============================================================================
*/


using System;

using WizardWrx;


namespace WizardWrx
{
    /// <summary>
    /// This displays the error messages that accompany exit codes.
    /// </summary>
    public class ErrorMessage
    {
        #region Private Constants
        /// <summary>
        /// Unless overridden, the display routines use this string as input to
        /// a string.Format derivative.
        /// </summary>
        const int ARRAY_ALLOCATE_NONE = 0;
        const int ARRAY_FIRST_ELEMENT = 0;
        const int ARRAY_INVALID_INDEX = -1;
        const int ARRAY_NEXT_ELEMENT = +1;
        const int ARRAY_SUBSCRIPT_TO_ORDINAL = +1;

        const string DEFAULT_FORMAT_STRING = @"Exit code {0}: {1}";

        const int ITEM_FOR_EXIT_CODE = ARRAY_FIRST_ELEMENT;
        const int ITEM_FOR_MESSAGE = ITEM_FOR_EXIT_CODE + ARRAY_NEXT_ELEMENT;
        const int ITEMS_RESERVED_FOR_STANDARD_FIELDS = ITEM_FOR_MESSAGE + ARRAY_NEXT_ELEMENT;
        #endregion  // Private Constants


        #region Public Constructors
        /// <summary>
        /// The default constructor creates an empty instance, and overrides its DisplayFormat property.
        /// </summary>
        public ErrorMessage ( )
        { }  // public ErrorMessage (1 of 4)


        /// <summary>
        /// Create a basic message.
        /// </summary>
        /// <param name="exitcode">
        /// Specify the associated exit code.
        /// 
        /// Please see the remarks.
        /// </param>
        /// <param name="message">
        /// Specify the associated message, which may contain format items.
        /// 
        /// Please see the remarks.
        /// </param>
        /// <remarks>
        /// When loading messages from a table that is indexed by exit code, 
        /// including the success exit code (zero), use a classic FOR loop to
        /// enumerate the elements, and the loop index can do double duty as the
        /// exit code.
        /// 
        /// Be aware that the constructors of this class specify the message
        /// first, followed by the exit code, because the message goes into the
        /// Label property of the base class, while the exit code goes into the
        /// Value property.
        /// </remarks>
        public ErrorMessage (
            uint exitcode ,
            string message )
        {
            Message = message;
            ExitCode = exitcode;
        }   // public ErrorMessage (2 of 4)


        /// <summary>
        /// Create a message that substitutes additional values supplied at run
        /// time.
        /// </summary>
        /// <param name="exitcode">
        /// Specify the associated exit code.
        /// 
        /// Please see the remarks.
        /// </param>
        /// <param name="message">
        /// Specify the associated message, which may contain format items.
        /// 
        /// Please see the remarks.
        /// </param>
        /// <param name="supplementarydetails">
        /// Use this argument to supply a list of one or more extra items to be
        /// substituted into the message at run time.
        /// </param>
        /// <remarks>
        /// When loading messages from a table that is indexed by exit code, 
        /// including the success exit code (zero), use a classic FOR loop to
        /// enumerate the elements, and the loop index can do double duty as the
        /// exit code.
        /// 
        /// Be aware that the constructors of this class specify the message
        /// first, followed by the exit code, because the message goes into the
        /// Label property of the base class, while the exit code goes into the
        /// Value property.
        /// </remarks>
        public ErrorMessage (
            uint exitcode ,
            string message ,
            object [ ] supplementarydetails )
        {
            Message = message;
            ExitCode = exitcode;
            SupplementaryDetails = supplementarydetails;
        }   // public ErrorMessage (3 of 4)


        /// <summary>
        /// Create a message that substitutes additional values supplied at run
        /// time.
        /// </summary>
        /// <param name="exitcode">
        /// Specify the associated exit code.
        /// 
        /// Please see the remarks.
        /// </param>
        /// <param name="message">
        /// Specify the associated message, which may contain format items.
        /// 
        /// Please see the remarks.
        /// </param>
        /// <param name="displayformat">
        /// Override the default DisplayFormat property.
        /// 
        /// Overriding this format string is independent of format item tokens
        /// that may be embedded in the message text.
        /// </param>
        /// <param name="supplementarydetails">
        /// Use this argument to supply a list of one or more extra items to be
        /// substituted into the message at run time.
        /// </param>
        /// <remarks>
        /// When loading messages from a table that is indexed by exit code, 
        /// including the success exit code (zero), use a classic FOR loop to
        /// enumerate the elements, and the loop index can do double duty as the
        /// exit code.
        /// 
        /// Be aware that the constructors of this class specify the message
        /// first, followed by the exit code, because the message goes into the
        /// Label property of the base class, while the exit code goes into the
        /// Value property.
        /// </remarks>
        public ErrorMessage (
            uint exitcode ,
            string message ,
            string displayformat ,
            object [ ] supplementarydetails )
        {
            Message = message;
            ExitCode = exitcode;
            DisplayFormat = displayformat;
            SupplementaryDetails = supplementarydetails;
        }   // public ErrorMessage (4 of 4)
        #endregion  // Public Constructors


        #region Public Instance Method Overrides
        /// <summary>
        /// Use the defaut format string, along with the exit code, message, and
        /// any SupplementaryDetails items stored with the instance to format
        /// and return a message string.
        /// </summary>
        /// <returns>
        /// The return value is a complete message, ready to display on a system
        /// console or in a message box, or to record in a log file.
        /// 
        /// Please see the Remarks.
        /// </returns>
        /// <remarks>
        /// Following is an outline of how I anticipate using this class.
        /// 
        /// 1) At startup, values known at that time, such as the initial
        /// working directory name or program startup time, version, etc., go
        /// into a static SupplementaryDetails arrat that belongs to all 
        /// instances.
        /// 
        /// 2) Additional information that is also known at startup, but is
        /// relevant for only a subset of messages, goes into a collection that
        /// is stored with the message in its instance SupplementaryDetails
        /// array.
        /// 
        /// 3) When the message is generated, last minute details that become
        /// available when the message is generated, such as, for example, the
        /// name of a file that cannot be opened, are fed into this method.
        /// 
        /// Here is how the two collections work together.
        /// 
        /// 1) Format item 0 is reserved for the exit code.
        /// 
        /// 2) Format item 1 is reserved for the predefined message.
        /// 
        /// 3) Additional format items, if any, map to elements of one or both
        /// SupplementaryDetails arrays, which are joined end to end.
        /// 
        /// 4) Any format item may appear in either the message format or the
        /// message, itself. Theoretically, a format item can appear in both,
        /// since the message is constructed by passing it through string.Format
        /// before passing it, along with the the format string stored directly
        /// or indirectly in the instance, to string.Format.
        /// </remarks>

        //  --------------------------------------------------------------------
        //  This method is marked New, rather thatn Override, because the 
        //  corresponding method in the base class is not marked as virtual,
        //  since I didn't anticipate inheriting from it. If I move the tested
        //  class into the same library (There is a good chance that I'll decide
        //  to install it there.), I'll fix it then. For the present, marking it
        //  new is equally effective for hiding the base class method.
        //  --------------------------------------------------------------------

        public string FormatDetail ( )
        {
            return FormatDetail (
                null ,              // supplementarydetails
                null );             // displayformat
        }   // public override string FormatDetail (1 of 3)


        /// <summary>
        /// Use the defaut format string, along with the exit code, message,
        /// any SupplementaryDetails items stored with the instance and passed
        /// into the method to format and return a message string.
        /// </summary>
        /// <param name="supplementarydetails">
        /// Specify addtional details, such as might be unavailable when the
        /// message collection is constructed, presumably at startup.
        /// </param>
        /// <returns>
        /// The return value is a complete message, ready to display on a system
        /// console or in a message box, or to record in a log file.
        /// 
        /// Please see the Remarks.
        /// </returns>
        /// <remarks>
        /// Following is an outline of how I anticipate using this class.
        /// 
        /// 1) At startup, values known at that time, such as the initial
        /// working directory name or program startup time, version, etc., go
        /// into a static SupplementaryDetails arrat that belongs to all 
        /// instances.
        /// 
        /// 2) Additional information that is also known at startup, but is
        /// relevant for only a subset of messages, goes into a collection that
        /// is stored with the message in its instance SupplementaryDetails
        /// array.
        /// 
        /// 3) When the message is generated, last minute details that become
        /// available when the message is generated, such as, for example, the
        /// name of a file that cannot be opened, are fed into this method.
        /// 
        /// Here is how the two collections work together.
        /// 
        /// 1) Format item 0 is reserved for the exit code.
        /// 
        /// 2) Format item 1 is reserved for the predefined message.
        /// 
        /// 3) Additional format items, if any, map to elements of one or both
        /// SupplementaryDetails arrays, which are joined end to end.
        /// 
        /// 4) Any format item may appear in either the message format or the
        /// message, itself. Theoretically, a format item can appear in both,
        /// since the message is constructed by passing it through string.Format
        /// before passing it, along with the the format string stored directly
        /// or indirectly in the instance, to string.Format.
        /// </remarks>
        public string FormatDetail (
            object [ ] supplementarydetails )
        {
            return FormatDetail (
                supplementarydetails ,  // supplementarydetails
                null );                 // displayformat
        }   // public override string FormatDetail (2 of 3)


        /// <summary>
        /// Use the supplied format string, along with the exit code, message,
        /// any SupplementaryDetails items stored with the instance and passed
        /// into the method to format and return a message string.
        /// </summary>
        /// <param name="supplementarydetails">
        /// Specify addtional details, such as might be unavailable when the
        /// message collection is constructed, presumably at startup.
        /// </param>
        /// <param name="displayformat">
        /// Specify the string to substitute for the DisplayFormat string that
        /// is otherwise visible to the instance.
        /// </param>
        /// <returns>
        /// The return value is a complete message, ready to display on a system
        /// console or in a message box, or to record in a log file.
        /// 
        /// Please see the Remarks.
        /// </returns>
        /// <remarks>
        /// Following is an outline of how I anticipate using this class.
        /// 
        /// 1) At startup, values known at that time, such as the initial
        /// working directory name or program startup time, version, etc., go
        /// into a static SupplementaryDetails arrat that belongs to all 
        /// instances.
        /// 
        /// 2) Additional information that is also known at startup, but is
        /// relevant for only a subset of messages, goes into a collection that
        /// is stored with the message in its instance SupplementaryDetails
        /// array.
        /// 
        /// 3) When the message is generated, last minute details that become
        /// available when the message is generated, such as, for example, the
        /// name of a file that cannot be opened, are fed into this method.
        /// 
        /// Here is how the two collections work together.
        /// 
        /// 1) Format item 0 is reserved for the exit code.
        /// 
        /// 2) Format item 1 is reserved for the predefined message.
        /// 
        /// 3) Additional format items, if any, map to elements of one or both
        /// SupplementaryDetails arrays, which are joined end to end.
        /// 
        /// 4) Any format item may appear in either the message format or the
        /// message, itself. Theoretically, a format item can appear in both,
        /// since the message is constructed by passing it through string.Format
        /// before passing it, along with the the format string stored directly
        /// or indirectly in the instance, to string.Format.
        /// </remarks>
        public string FormatDetail (
            object [ ] supplementarydetails , 
            string displayformat )
        {
            object [ ] raobjEverything = new object [
                ITEMS_RESERVED_FOR_STANDARD_FIELDS
                + ( ( ErrorMessage.DetailFormatItems == null )
                    ? ARRAY_ALLOCATE_NONE
                    : ErrorMessage.DetailFormatItems.Length )
                + ( ( this.SupplementaryDetails == null )
                    ? ARRAY_ALLOCATE_NONE
                    : this.SupplementaryDetails.Length )
                + ( ( supplementarydetails == null )
                    ? ARRAY_ALLOCATE_NONE
                    : supplementarydetails.Length ) ];

            raobjEverything [ ITEM_FOR_MESSAGE ] = this.ExitCode;
            raobjEverything [ ITEM_FOR_EXIT_CODE ] = this.Message;

            int intNextOpenSlot = ITEMS_RESERVED_FOR_STANDARD_FIELDS;

            if ( ErrorMessage.DetailFormatItems != null )
            {
                ErrorMessage.DetailFormatItems.CopyTo (
                    raobjEverything ,
                    intNextOpenSlot );
                intNextOpenSlot += ErrorMessage.DetailFormatItems.Length;
            }   // if ( ErrorMessage.DetailFormatItems != null )

            if ( this.SupplementaryDetails != null )
            {
                this.SupplementaryDetails.CopyTo (
                    raobjEverything ,
                    intNextOpenSlot );
                intNextOpenSlot += this.SupplementaryDetails.Length;
            }   // if ( this.SupplementaryDetails != null )

            if ( supplementarydetails != null )
            {
                supplementarydetails.CopyTo (
                    raobjEverything ,
                    intNextOpenSlot );
                intNextOpenSlot += supplementarydetails.Length;
            }   // if ( supplementarydetails != null )

            if ( string.IsNullOrEmpty ( displayformat ) )
                return string.Format (
                    this.DisplayFormat ,
                    this.ExitCode ,
                    string.Format (
                        this.Message ,
                        raobjEverything ) );
            else
                return string.Format (
                    displayformat ,
                    this.ExitCode ,
                    string.Format (
                        this.Message ,
                        raobjEverything ) );
        }   // public override string FormatDetail (3 of 3)
        #endregion  // Public Instance Method Overrides


        #region Public Properties
        /// <summary>
        /// The instance display format gets its own string, which is NULL,
        /// unless the instance has a dedicated message format string.
        /// </summary>
        string _strDisplayFormat = null;


        /// <summary>
        /// Return the custom instance format string if there is one. Otherwise,
        /// return the default string stored in in the shared string.
        /// 
        /// New values are accepted without question, and a null or empty string
        /// reinstates the default format.
        /// </summary>
        public string DisplayFormat
        {
            get
            {   // This construct replaces a block IF statement that feels out of place in a property.
                return string.IsNullOrEmpty ( _strDisplayFormat ) 
                    ? s_strDefaultFormatString 
                    : _strDisplayFormat;
            }   // public string DisplayFormat get method

            set { _strDisplayFormat = value; }
        }   // public string DisplayFormat Property


        /// <summary>
        /// The message string is accepted at face value.
        /// </summary>
        public string Message { get; set; }


        /// <summary>
        /// This instance property gets or sets a collection of items to be
        /// incorporated into error messages at run time. Use this property to
        /// store values known at program startup.
        /// </summary>
        public object [ ] SupplementaryDetails { get; set; }


        /// <summary>
        /// The exit code is accepted at face value. Note, however, that only
        /// positive integers are supported, since this class relies upon it as
        /// an indexer.
        /// </summary>
        public uint ExitCode { get; set; }
        #endregion  // Public Properties


        #region Static Properties
        /// <summary>
        /// The private storage for the default format string is initialized
        /// from a private constant.
        /// </summary>
        static string s_strDefaultFormatString = DEFAULT_FORMAT_STRING;


        /// <summary>
        /// This static (Shared in Visual Basic) property gets or sets the
        /// default message format string. Passing a null reference (Nothing in
        /// Visual Basic) or the empty string resets the property to its hard
        /// coded default value.
        /// </summary>
        public static string DefaultFormatString
        {
            get { return s_strDefaultFormatString; }

            set
            {
                if ( string.IsNullOrEmpty ( value ) )
                    s_strDefaultFormatString = DEFAULT_FORMAT_STRING;
                else 
                    s_strDefaultFormatString = value;
            }   // public static string DefaultFormatString get method
        }   // public static string DefaultFormatString Property


        /// <summary>
        /// This static (Shared in Visual Basic) property gets or sets the
        /// default array of additional format items to be incorporated into
        /// some or all messages. Passing a null reference (Nothing in Visual 
        /// Basic) destroys the array.
        /// </summary>
        public static object [ ] DetailFormatItems { get; set; }
        #endregion  // Static Properties
    }   // class ErrorMessage
}   // partial namespace WizardWrx