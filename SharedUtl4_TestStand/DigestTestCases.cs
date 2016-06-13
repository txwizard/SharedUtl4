/*
    ============================================================================

    Namespace:          SharedUtl4_TestStand

    Class Name:         DigestTestCases

    File Name:          DigestTestCases.cs

    Synopsis:           Hide the details of how the file names and test values
                        are stored.

    Remarks:            In point of fact, they live in a labeled, tab delimited
                        text file.

    License:            Copyright (C) 2012-2016, David A. Gray. 
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

    Created:            Monday, 17 February 2014

    ----------------------------------------------------------------------------
    Revision History
    ----------------------------------------------------------------------------

    Date       Version Author Description
    ---------- ------- ------ --------------------------------------------------
    2014/02/17 2.2     DAG    Initial implementation.
    
	2014/07/31 2.7     DAG    Move the test case file into the assembly, as a
                              manifest resources, which also exercises the new
                              LoadTextFileFromEntryAssembly method used to load
                              it from the entry point assembly, which tests two
                              of the three new methods for loading resources
                              from assemblies.

	2016/06/05 3.0     DAG    Break the dependency on WizardWrx.SharedUtl2.dll,
                              correct misspelled words flagged by the spelling
                              checker add-in, and incorporate my three-clause
                              BSD license, and document why I suppressed warning
                              CS0618. Please see item 2 in Remarks (above).
    ============================================================================
*/


using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

/* Added by DAG */

using System.IO;

using WizardWrx;


namespace SharedUtl4_TestStand
{
    internal class DigestTestCases
    {
        const string EMPTY = @"Input file {0} is empty.";
        const string FNF = @"Input file {0} cannot be found.";
        const string INVALID_RECORD = @"Input file {0}, record {1} is invalid.";

        public struct CaseRecord
        {
            public string strFileName;
            public string strDigest;
        };  // public struct CaseRecord

        private List<CaseRecord> _lstCaseRecords;

        public DigestTestCases ( )        
        {
            const int LABEL_ROW = 1;
            const int FIELD_FILE_NAME = ArrayInfo.ARRAY_FIRST_ELEMENT;
            const int FIELD_EXPECTED_DIGEST = FIELD_FILE_NAME + MagicNumbers.PLUS_ONE;
            const int TOTAL_FIELDS = FIELD_EXPECTED_DIGEST + MagicNumbers.PLUS_ONE;

            const string TEST_CASE_FILENAME = @"DigestMD5TestCases.TXT";

            try
            {
                string [ ] astrCases = Utl.LoadTextFileFromEntryAssembly ( TEST_CASE_FILENAME );
                   
                int intNRecords = astrCases.Length;

                if ( intNRecords > LABEL_ROW )
                {   // File contains detail records.
                    _lstCaseRecords = new List<CaseRecord> ( intNRecords - LABEL_ROW );

                    for ( int intRecordNumber = LABEL_ROW ; intRecordNumber < intNRecords ; intRecordNumber++ )
                    {   // Skipping the label row, which is for human consumption, populate the list from the data records.
                        string [ ] astrFields = astrCases [ intRecordNumber ].Split ( new char [ ] { SpecialCharacters.TAB_CHAR } );

                        if ( astrFields.Length == TOTAL_FIELDS )
                        {
                            CaseRecord cr = new CaseRecord ( );
                            cr.strFileName = astrFields [ FIELD_FILE_NAME ];
                            cr.strDigest = astrFields [ FIELD_EXPECTED_DIGEST ];
                            _lstCaseRecords.Add ( cr );
                        }
                        else
                        {
                            throw new ArgumentException (
                                string.Format (
                                INVALID_RECORD ,
                                TEST_CASE_FILENAME ,
                                intRecordNumber ) );
                        }   // if ( astrFields.Length == EXPECTED_FIELD_COUNT )
                    }   // for ( int intRecordNumber = LABEL_ROW ; intRecordNumber < intNRecords ; intNRecords++ )
                }
                else
                {   // File is empty.
                    throw new ArgumentException ( 
                        string.Format (
                            EMPTY ,
                            TEST_CASE_FILENAME ) );
                }   // if ( intNRecords > LABEL_ROW )
            }
            catch
            {
                throw;
            }
        }   // public DigestTestCases

        public List<CaseRecord> Cases { get { return _lstCaseRecords; } }

        public int NCases { get { return _lstCaseRecords.Count; } }
    }   // class DigestTestCases
}   // partial namespace SharedUtl4_TestStand