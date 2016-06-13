/*
    ============================================================================

    Namespace:          WizardWrx

    Class Name:         ErrorMessagesCollection

    File Name:          ErrorMessagesCollection.cs

    Synopsis:           This derivative of WizardWrx.ReportDetails specializes
                        in storing and displaying the error messages that have
                        accompanying exit codes.

    Remarks:            Since the runtime supplies one that does everything we
                        need, the default constructor is omitted. Even if it is
                        marked as public, the two that I expect to use are the
                        two, provided here, that do useful work.

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

	2016/06/03 3.0     DAG    Break the dependency on WizardWrx.SharedUtl2.dll,
                              correct misspelled words flagged by the spelling
                              checker add-in, and incorporate my three-clause
                              BSD license.
    ============================================================================
*/


using System;
using System.Collections.Generic;
using System.Text;

namespace WizardWrx
{
    /// <summary>
    /// This class is a specialized derivative of ReportDetails; its specialty
    /// is managing error messages that correspond to exit codes.
    /// </summary>
    public class ErrorMessagesCollection : List<ErrorMessage>
    {
        /// <summary>
        /// The default exit code of a Windows program is zero.
        /// </summary>
        public const int ERR_SUCCESS = 0;


        /// <summary>
        /// Exit code 1 is reserved for misceallaneous runtime exceptions.
        /// </summary>
        public const int ERR_RUNTIME = 1;


        /// <summary>
        /// Create the collection, and reserve room for the expected number of
        /// messages.
        /// </summary>
        /// <param name="capacity">
        /// Specify the capacity, which might be the length of the array that
        /// contains the messages. Better yet, use the next overload, which
        /// accepts the whole array, from which it derives the capacity.
        /// </param>
        public ErrorMessagesCollection ( int capacity )
            : base ( capacity )
        { } // public ErrorMessagesCollection (1 of 2)


        /// <summary>
        /// Create the collection, and load an array of string into it.
        /// </summary>
        /// <param name="messages">
        /// Specify the array of strings to load into the collection.
        /// </param>
        public ErrorMessagesCollection ( string [ ] messages )
            : base ( messages.Length )
        {
            int intMessageCount = messages.Length;      // Every iteration tests this value.

            for ( uint uintExitCode = ERR_SUCCESS ;
                       uintExitCode < intMessageCount ;
                       uintExitCode++ )
            {
                base.Add ( new ErrorMessage (
                    uintExitCode ,
                    messages [ uintExitCode ] ) );
            }   // for ( int intExitCode = ERR_SUCCESS ; intExitCode < intMessageCount ; intExitCode++ )
        }   // public ErrorMessagesCollection (2 of 2)


        /// <summary>
        /// Get the message returned by the default FormatDetail method on the
        /// specified ErrorMessage object.
        /// </summary>
        /// <param name="exitcode">
        /// Specify the exit code, cast to the unsigned integer type expected by
        /// the ErrorExit method on the application state manager.
        /// </param>
        /// <returns>
        /// The return value is a complete message, ready to display on a system
        /// console or in a message box, or to record in a log file.
        /// 
        /// Please see the Remarks on <see cref="ErrorMessage"/>.
        /// </returns>
        public string FormatDetail ( uint exitcode )
        {
            return this [ ( int ) exitcode ].FormatDetail ( );
        }   // public string FormatDetail (1 of 3)


        /// <summary>
        /// Get the message returned by the FormatDetail method on the specified
        /// ErrorMessage object that takes a SupplementaryDetails array.
        /// </summary>
        /// <param name="exitcode">
        /// Specify the exit code, cast to the unsigned integer type expected by
        /// the ErrorExit method on the application state manager.
        /// </param>
        /// <param name="supplementarydetails">
        /// Specify addtional details, such as might be unavailable when the
        /// message collection is constructed, presumably at startup.
        /// </param>
        /// <returns>
        /// The return value is a complete message, ready to display on a system
        /// console or in a message box, or to record in a log file.
        /// 
        /// Please see the Remarks on <see cref="ErrorMessage"/>.
        /// </returns>
        public string FormatDetail (
            uint exitcode ,
            object [ ] supplementarydetails )
        {
            return this [ ( int ) exitcode ].FormatDetail ( supplementarydetails );
        }   // public string FormatDetail (2 of 3)


        /// <summary>
        /// Get the message returned by the FormatDetail method on the specified
        /// ErrorMessage object that takes a SupplementaryDetails array and a
        /// custom format string.
        /// </summary>
        /// <param name="exitcode">
        /// Specify the exit code, cast to the unsigned integer type expected by
        /// the ErrorExit method on the application state manager.
        /// </param>
        /// <param name="supplementarydetails"></param>
        /// <param name="displayformat">
        /// Specify a new format string to override the default string assigned
        /// to the class.
        /// </param>
        /// <returns>
        /// The return value is a complete message, ready to display on a system
        /// console or in a message box, or to record in a log file.
        /// 
        /// Please see the Remarks on <see cref="ErrorMessage"/>.
        /// </returns>
        public string FormatDetail ( 
            uint exitcode ,
            object [ ] supplementarydetails ,
            string displayformat )
        {
            return this [ ( int ) exitcode ].FormatDetail (
                supplementarydetails ,
                displayformat );
        }   // public string FormatDetail (3 of 3)
    }   // public class ErrorMessagesCollection
}   // partial namespace WizardWrx