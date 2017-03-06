/*
    ============================================================================

    Namespace:          WizardWrx

    Class Name:         StringThings

    File Name:          StringThings.cs

    Synopsis:           This class exposes static methods to perform frequently
                        required tasks that involve parsing or modifying strings
                        such as command line arguments coming in and messages
                        going out.

    Remarks:            Like everything else in this library, these methods pick
                        up where similar classes in WizardWrx.SharedUtl2, which
                        is frozen, leave off.

    Author:             David A. Gray

    License:            Copyright (C) 2014-2017, David A. Gray. 
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

    Created:            Friday, 18 July 2014

    ----------------------------------------------------------------------------
    Revision History
    ----------------------------------------------------------------------------

    Date       Version Author Description
    ---------- ------- ------ --------------------------------------------------
    2014/07/18 2.5     DAG    This class makes its debut.

	2016/06/05 3.0     DAG    Break the dependency on WizardWrx.SharedUtl2.dll,
                              correct misspelled words flagged by the spelling
                              checker add-in, and incorporate my three-clause
                              BSD license.

	2017/03/04 3.1     DAG    Eliminate dependence on WizardWrx.ConsoleAppAids2
                              in ReportUnresolvedEnvironmentStrings by changing
                              its return type from void to unsigned integer, and
                              passing the specified exit code through, so that
                              the call can be wrapped inside the ultimate exit
                              routine.
    ============================================================================
*/


using System;
using System.Text;


namespace WizardWrx
{
    /// <summary>
    /// Provide static methods to perform frequently required tasks that involve
    /// parsing or modifying strings such as command line arguments coming in
    /// and messages going out.
    /// </summary>
    public static class StringThings
    {
        const string ENV_STR_DLM = @"%";


        /// <summary>
        /// If the count of objects to which a noun refers is greater than 1,
        /// replace its singular form with its plural form. Use this method to
        /// generate grammatically correct sentences in which the noun's number
        /// is grammatically correct.
        /// </summary>
        /// <param name="puintNumber">
        /// Base the adjustment on this number.
        /// </param>
        /// <param name="pstrNounSingular">
        /// Specify the noun to adjust, which is assumed to be in its singular
        /// form, and that its plural is the same word with the letter "S"
        /// appended.
        /// </param>
        /// <param name="pstrPhrase">
        /// Replace all instances of pstrNoun in this string with the plural of
        /// pstrNoun if pintNumber is greater than 1.
        /// </param>
        /// <param name="pstrPluralForm">
        /// Specify the plural form of pstrSingularForm, either outright or as a
        /// plus sign followed immediately by the suffix to append.
        /// 
        /// If this arguments is a null reference or the empty string, the
        /// hard coded default suffix, a lower case s, is appended.
        /// </param>
        /// <returns>
        /// The return value is pstrPhrase, amended if needed to reflect the
        /// correct number for pstrNoun.
        /// </returns>
        public static string AdjustNumberOfNoun (
            uint puintNumber ,
            string pstrNounSingular ,
            string pstrPluralForm ,
            string pstrPhrase )
        {
            const uint SINGULAR = 1;
            const int FIRST_CHARACTER_IN_STRING = 0;
            const int STARTING_AT_SECOND_CHARACTER = 1;
            const char PLURAL_FORM_IS_SUFFIX = '+';
            const char PLURAL_SUFFIX_DEFAULT = 's';

            if ( puintNumber > SINGULAR )
            {
                if ( string.IsNullOrEmpty ( pstrPluralForm ) )
                {   // Append the default suffix.
                    return pstrPhrase.Replace (
                        pstrNounSingular ,
                        string.Concat (
                            pstrNounSingular ,
                            PLURAL_SUFFIX_DEFAULT ) );
                }   // TRUE (degenerate case) block, if ( string.IsNullOrEmpty ( pstrPluralForm ) )
                else
                {   // Override the default suffix.
                    if ( pstrPluralForm [ FIRST_CHARACTER_IN_STRING ] == PLURAL_FORM_IS_SUFFIX )
                    {   // The plural form of the noun requires a suffix.
                        return pstrPhrase.Replace (
                            pstrNounSingular ,
                            string.Concat (
                                pstrNounSingular ,
                                pstrPluralForm.Substring ( STARTING_AT_SECOND_CHARACTER ) ) );
                    }   // TRUE block, if ( pstrPluralForm [ FIRST_CHARACTER_IN_STRING ] == PLURAL_FORM_IS_SUFFIX )
                    else
                    {   // The plural form is a best handled by substitution.
                        return pstrPhrase.Replace (
                            pstrNounSingular ,
                            pstrPluralForm );
                    }   // iFALSE block, if ( pstrPluralForm [ FIRST_CHARACTER_IN_STRING ] == PLURAL_FORM_IS_SUFFIX )
                }   // FALSE (standard case) block, if ( string.IsNullOrEmpty ( pstrPluralForm ) )
            }   // TRUE (standard case) block, if ( puintNumber > SINGULAR )
            else
            {   // Leave the noun singular.
                return pstrPhrase;
            }   // FALSE (degenerate case) block, if ( puintNumber > SINGULAR )
        }   // public static string AdjustNumberOfNoun


        /// <summary>
        /// Scan a string for environment string delimiter characters left
        /// behind by an environment string expansion.
        /// </summary>
        /// <param name="pstrInput">
        /// Specify a string that has had its environment strings expanded.
        /// </param>
        /// <returns>
        /// The return value is the count of remaining environment string
        /// delimiters. Please see the remarks for additional information.
        /// </returns>
        /// <remarks>
        /// There are two reasons that such delimiters might be left behind.
        /// 
        /// 1) The input string contains environment strings that have no like
        /// named strings in the environment block that belongs to the process.
        /// 
        /// 2) The input string contains a malformed string that is missing one
        /// of its delimiting tokens.
        /// 
        /// This routine is a wrapper for WizardWrx.StringTricks.CountSubstrings
        /// that supplies the required token. Since you could as well call that
        /// routine directory, this routine is syntactic sugar.
        /// </remarks>
        /// <seealso cref="ReportUnresolvedEnvironmentStrings"/>
        public static uint CountUnresolvedEnvironmentStrings ( string pstrInput )
        {
            return ( uint ) WizardWrx.StringTricks.CountSubstrings (
                pstrInput ,
                ENV_STR_DLM );
        }   // UnresolvedEnvironmentStrings


        /// <summary>
        /// Display a string that contains unmatched environment strings or
        /// unmatched environment string delimiters, followed by details about
        /// the locations of the errors.
        /// </summary>
        /// <param name="pstrInput">
        /// Specify a string that has had its environment strings expanded.
        /// </param>
        /// <param name="puintNEnvStrDlms">
        /// Specify the count of unmatched delimiters. A companion routine,
        /// UnresolvedEnvironmentStrings, can deliver the count, although the
        /// call cannot be nested. Please see the remarks.
        /// </param>
        /// <param name="puintExitCode">
        /// This routine is intended to report the error and exit the calling
        /// console application, returning the specified value as its exit code.
        /// </param>
		/// <returns>
		/// The exit code is passed through, so that the control need not return
		/// to the caller, but may exit through Environment.Exit, either
		/// directly or indirectly.
		/// </returns>
        /// <remarks>
        /// This routine never returns control to its caller. Hence, the calling
        /// routine must capture the count returned by companion method
        /// UnresolvedEnvironmentStrings and call this routine only if it is
        /// greater than zero. The reasoning behind this is that the program
        /// should not proceed with the specified inputs.
        /// </remarks>
        /// <see cref="CountUnresolvedEnvironmentStrings"/>
        public static uint ReportUnresolvedEnvironmentStrings (
            string pstrInput ,
            uint puintNEnvStrDlms ,
            uint puintExitCode )
        {
            const uint FIRST_ERROR = 1;
            const int POS_TO_ORD = 1;
            const int POS_START = 0;

            StringBuilder sbMessage = new StringBuilder ( MagicNumbers.CAPACITY_01KB );

            sbMessage.AppendFormat (
                AdjustNumberOfNoun (                                                // Format string (message template)
                    puintNEnvStrDlms ,                                                  // uint puintNumber         = number of items
                    Properties.Resources.ERRMSG_VARIABLE_LITERAL ,                      // string pstrSingularForm  = singular form of noun
                    null ,                                                              // string pstrPluralForm    = plural form of noun, or suffix
                    Properties.Resources.ERRMSG_UNRESLOVED_ENVIRONEMT_STRINGS ) ,       // string pstrPhrase        = phrase in which the noun appears
                WizardWrx.StringTricks.QuoteString (                                // Format Item 0
                    pstrInput ) ,                                                       // Surround with double quotation marks
                puintNEnvStrDlms ,
                Environment.NewLine );                                              // Format Item 1

            int intLastPos = POS_START;

            for ( uint uintOccurrence = FIRST_ERROR ;
                        uintOccurrence == puintNEnvStrDlms ;
                        uintOccurrence++ )
            {
                intLastPos = pstrInput.IndexOf (
                    ENV_STR_DLM ,
                    intLastPos );

                if ( uintOccurrence < puintNEnvStrDlms )
                {   // All but last error get a message that says that more are coming.
                    sbMessage.AppendFormat (
                        Properties.Resources.ERRMSG_START_CHARACTER ,               // Format string (message template)
                        intLastPos + POS_TO_ORD ,                                   // Format Item 0 = position of error
                        Properties.Resources.ERRMSG_COMMA_AND_LITERAL ,             // Format Item 1 = more to follow
                        Environment.NewLine );                                      // Format Item 2 = newline
                }   // TRUE (all but last error) block, if ( uintOccurrence < puintNEnvStrDlms )
                else
                {   // This is the last (or only) error.
                    sbMessage.AppendFormat (
                        Properties.Resources.ERRMSG_START_CHARACTER ,               // Format string (message template)
                        intLastPos + POS_TO_ORD ,                                   // Format Item 0 = position of error
                        string.Empty ,                                              // Format Item 1 = nothing (omit)
                        Environment.NewLine );                                      // Format Item 2 = newline
                }   // FALSE (last error) block, if ( uintOccurrence < puintNEnvStrDlms )
            }   // for ( uint uintOccurrence = FIRST_ERROR ; uintOccurrence == puintNEnvStrDlms ; uintOccurrence++ )

            Console.Error.WriteLine ( sbMessage );      // Write the whole message in one go. ToString happens implictly.
			return puintExitCode;
        }   // public static void ReportUnresolvedEnvironmentStrings
    }   // public static class StringThings
}   // partial namespace WizardWrx