/*
    ============================================================================

    Module Name:        Program.cs

    Namespace Name:     SharedUtl4_TestStand

    Class Name:         Program

    Synopsis:           This class module defines the main routine of a test
                        stand program for the WizardWrx.SharedUtl4 class library
                        of proven routines gathered from various production
                        programs.

    Remarks:            1)	Since this is tested code, I started the version 
							number at 2.0.

						2)	Warning CS0618 is disabled for this assembly so that
							I won't be distracted the presence of the remaining
                            two warnings, covering MD5Hash and SHA1Hash, both
							deprecated message digest algorithms. I know they
							are, and so should anybody who knows enough to use
							the message digest algorithms for which the classes
							in the WizardWrx.Cryptography namespace provide
							convenience wrappers.

						3)	Anything that can be initialized at compile time is,
                            and anything that should never change at run time is
                            marked as read only, thereby asking the compiler to
                            enforce it.

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

    Created:            Monday, 19 November 2012 - Wednesday, 21 November 2012

    ----------------------------------------------------------------------------
    Revision History
    ----------------------------------------------------------------------------

    Date       Version Author Synopsis
    ---------- ------- ------ --------------------------------------------------
    2012/11/21 2.0     DAG    Application created and tested.
    
    2012/01/05 2.1     DAG    Extend to test the overloaded detail label 
                              generator.
    
    2014/02/18 2.2     DAG    Extend to test the MD5Hash method of a new
                              DigestFile class.
    
    2014/06/29 2.3     DAG    Add coverage of the ReportDetails collection and
                              its members, along with the ability to run tests
                              selectively.
    
    2014/07/08 2.4     DAG    Add tests of a new ReportHelpers.UpgradeFormatItem
                              method, and upgrade the support libraries to the
                              new series 2 set.
    
    2014/07/19 2.5     DAG    Test the capability to sort the report items and a
                              new FormatStringParser class that will eventually
                              be broken off into WizardWrx.SharedUtl4.dll or a
                              dedicated library.

    2014/07/21 2.6     DAG    Revert to processing string lengths as signed
                              integers, even though the idea goes against every
                              fiber of my being, since a negative value for the
                              length of a string is utter nonsense.

    2014/07/31 2.7     DAG    Remove DigestMD5TestCases from the settings file,
                              since the associated file has become a manifest
                              resource, and the DigestFileTests routine uses it
                              to validate the new routines that I developed for
                              loading sucH resources from any assembly.

    2014/09/21 2.8     DAG    Extend the DigestFileTests method to cover string
                              digests, using the same files as inputs.
 
	2016/06/12 3.0     DAG    1) Break a dependency on WizardWrx.SharedUtl2.dll,
                                 correct misspelled words flagged by the spell
                                 check add-in, and incorporate my three-clause
                                 BSD license, and document why I suppressed 
                                 warning CS0618.

                                 Please see item 2 in Remarks (above).

                              2) Add an option to dump the ASCII table. Treat it
                                 as another test set.
    ============================================================================
*/


using System;
using System.Collections.Generic;
using System.Text;

/*  Added by DAG */

using System.IO;

using WizardWrx;
using WizardWrx.ConsoleAppAids2;
using WizardWrx.DLLServices2;
using WizardWrx.FormatStringEngine;
using WizardWrx.Cryptography;


namespace SharedUtl4_TestStand
{
    class Program
    {
        const int ERR_RUNTIME = MagicNumbers.PLUS_ONE;

		//	--------------------------------------------------------------------
		//	At compile time, the following strings are loaded into a static read
		//	only table (array), s_astrTestSelections. The strings that follow
		//	are subscripts into the table, which are passed to method DoThisTest
		//	to indicate which test is up next for selection to be executed or
		//	skipped. My thinking is that integers are more efficient than string
		//	pointers to pass around among subroutines.
		//
		//	The line comment on each string is the name of the routine that runs
		//	when its test is selected. Unqualified methods are defined as static
		//	methods in this class.
		//	--------------------------------------------------------------------

		const string TEST_MAXSTRINGLENGTH = @"MaxStringLength";                 // MaxStringLength_Tests
		const string TEST_MERGENEWITEMSINTOARRAY = @"MergeNewItemsIntoArray";   // MergeNewItemsIntoArray_Tests
		const string TEST_DIGESTFILE = @"DigestFile";                           // DigestFileTests
        const string TEST_REPORTDETAILS = @"ReportDetails";                     // ReportDetailsTests
        const string TEST_REPORTHELPERS = @"ReportHelpers";                     // ReportHelpers
		const string TEST_FORMAT_ITEM_GEN = @"FormatItemGen";                   // FormatStringParsing_Drills.TestFormatItemBuilders
		const string TEST_ASCII_TABLE_GEN = @"ASCIITableGen";					// FormatStringParsing_Drills.ASCII_Table_Gen 

		const int CHOICE_MAXSTRINGLENGTH = 0;
		const int CHOICE_MERGENEWITEMSINTOARRAY = 1;
		const int CHOICE_DIGESTFILE = 2;
		const int CHOICE_REPORTDETAILS = 3;
		const int CHOICE_REPORTHELPERS = 4;
		const int CHOICE_FORMAT_ITEM_GEN = 5;
		const int CHOICE_ASCII_TABLE_GEN = 6;

		internal static readonly string [ ] s_astrTestSelections =
		{
			TEST_MAXSTRINGLENGTH ,
			TEST_MERGENEWITEMSINTOARRAY ,
			TEST_DIGESTFILE ,
			TEST_REPORTDETAILS ,
			TEST_REPORTHELPERS ,
			TEST_FORMAT_ITEM_GEN ,
			TEST_ASCII_TABLE_GEN
		};	// s_astrTestSelections

        const bool TEST_SORTED = true;
        const bool TEST_UNSORTED = false;

        internal static readonly ConsoleAppStateManager rs_appThis = ConsoleAppStateManager.GetTheSingleInstance ( );
        internal static readonly string rs_strDataDirectory = SharedUtl4_TestStand.Properties.Settings.Default.Data_Directory;

        private static FormatItem.Alignment [ ] s_aenmAlignment =
        {
            FormatItem.Alignment.Left ,
            FormatItem.Alignment.Right
        };  // private static FormatItem.Alignment [ ] s_aenmAlignment

        private static readonly string [ ] s_astrFormats =
        {
            NumericFormats.NUMBER_PER_REG_SETTINGS_0D ,
            NumericFormats.HEXADECIMAL_2 ,
            NumericFormats.HEXADECIMAL_4 ,
            NumericFormats.HEXADECIMAL_8
        };  // [ ] s_astrFormats

		internal static readonly int s_intNTests = s_astrTestSelections.Length;
		internal static string [ ] s_astrPgmArgs;			// This gets a copy of the command line arguments.
        internal static int s_intNArgs;						// This gets the count of command line arguments.


        static void Main ( string [ ] args )
        {
            rs_appThis.DisplayBOJMessage ( );
            Console.WriteLine (
                SharedUtl4_TestStand.Properties.Resources.IDS_DATA_DIRECTORY_NAME ,
                rs_strDataDirectory );
            rs_appThis.BaseStateManager.LoadErrorMessageTable (
                new string [ ]
                {
                    Properties.Resources.ERRMSG_SUCCESS ,
                    Properties.Resources.ERRMSG_RUNTIME
                } );
            rs_appThis.BaseStateManager.AppExceptionLogger.OptionFlags =
                rs_appThis.BaseStateManager.AppExceptionLogger.OptionFlags
                | ExceptionLogger.OutputOptions.EventLog
                | ExceptionLogger.OutputOptions.Stack
                | ExceptionLogger.OutputOptions.StandardOutput;
            InitializeTestSelections ( args );

            try
            {
                if ( DoThisTest ( CHOICE_MAXSTRINGLENGTH ) )
                    MaxStringLength_Tests ( );

                if ( DoThisTest ( CHOICE_MERGENEWITEMSINTOARRAY ) )
                    MergeNewItemsIntoArray_Tests ( );

                if ( DoThisTest ( CHOICE_DIGESTFILE ) )
                    DigestFileTests ( );

                if ( DoThisTest ( CHOICE_REPORTDETAILS ) )
                {
                    ReportDetailsTests ( TEST_UNSORTED );
                    ReportDetailsTests ( TEST_SORTED );
                }   // if ( DoThisTest ( CHOICE_REPORTDETAILS ) )

                if ( DoThisTest ( CHOICE_REPORTHELPERS ) )
                    ReportHelpersTests ( );

                if ( DoThisTest ( CHOICE_FORMAT_ITEM_GEN ) )
                    FormatStringParsing_Drills.TestFormatItemBuilders ( );

				if ( DoThisTest ( CHOICE_ASCII_TABLE_GEN ) )
					FormatStringParsing_Drills.ASCII_Table_Gen ( );
            }
            catch ( Exception errAllKinds )
            {
				rs_appThis.BaseStateManager.AppExceptionLogger.ReportException ( errAllKinds );
                rs_appThis.BaseStateManager.AppReturnCode = ERR_RUNTIME;
            }
            finally
            {
                if ( rs_appThis.BaseStateManager.AppReturnCode == MagicNumbers.ERROR_SUCCESS )
                {
                    rs_appThis.NormalExit (
                        ConsoleAppStateManager.NormalExitAction.WaitForOperator );
                }   // TRUE (expected outcome) block, if ( rs_appThis.BaseStateManager.AppReturnCode == MagicNumbers.ERROR_SUCCESS )
                else
                {
                    rs_appThis.ErrorExit (
                        ( uint ) rs_appThis.BaseStateManager.AppReturnCode );
                }   // FALSE (exception) block, if ( rs_appThis.BaseStateManager.AppReturnCode == MagicNumbers.ERROR_SUCCESS )
            }
        }   // static void Main


        private static string CompareValues (
            string pstrComputedValue ,
            string pstrExpectedValue )
        {
            if ( pstrComputedValue == pstrExpectedValue )
                return Properties.Resources.IDS_TEST_PASS;
            else
                return Properties.Resources.IDS_TEST_FAIL;
        }   // CompareValues


        private static void DigestFileTests ( )
        {
            const string PROLOGUE = @"DigestFileTests Begin: Files to test = {0}{1}";
            const string REPORT = @"    File # {0}: Name           = {1}{5}              Length         = {2}{5}              MD5 Digest     = {3} - {4}";

            //            const string SHA_RPT = @"              SHA-{0} Digest = {1} - {2}{3}";    // With PASS flag
            const string SHA_RPT = @"              SHA-{0} Digest = {1}{2}";            // W/O PASS flag
            const string SHA_STRINGS = @"              Repeating SHA-256, SHA-384, and SHA-512 with same data as strings.{0}";
            const string EPILOGUE = @"DigestFileTests Done!";

            DigestTestCases tcList = new DigestTestCases ( );
            int intFileNumber = MagicNumbers.ZERO;

            Console.WriteLine (
                PROLOGUE ,
                tcList.NCases ,
                Environment.NewLine );

            foreach ( DigestTestCases.CaseRecord cr in tcList.Cases )
            {
                FileInfo fiThisFile = new FileInfo ( cr.strFileName );
                long lngFileLength = fiThisFile.Length;
                string strComputedValue = DigestFile.MD5Hash ( cr.strFileName );
                string strExpectedValue = cr.strDigest ;

                Console.WriteLine (
                    REPORT ,
                    new object [ ]
                        {
                            ++intFileNumber ,
                            cr.strFileName ,
                            lngFileLength ,
                            strComputedValue ,
                            CompareValues(
                                strComputedValue,
                                strExpectedValue) ,
                            Environment.NewLine
                        } );

                Console.WriteLine (
                    SHA_RPT , 
                    "1  " , 
                    DigestFile.SHA1Hash ( cr.strFileName ) ,
                    string.Empty );

                Console.WriteLine (
                    SHA_RPT ,
                    "256" ,
                    DigestFile.SHA256Hash ( cr.strFileName ) ,
                    string.Empty );
                Console.WriteLine (
                    SHA_RPT ,
                    "384" ,
                    DigestFile.SHA384Hash ( cr.strFileName ) ,
                    string.Empty );
                Console.WriteLine (
                    SHA_RPT ,
                    "512" ,
                    DigestFile.SHA512Hash ( cr.strFileName ) ,
                    Environment.NewLine );

                Console.WriteLine (
                    SHA_STRINGS ,
                    Environment.NewLine );

                //  ------------------------------------------------------------
                //  Load the whole file, in one gulp, into one long string.
                //  ------------------------------------------------------------

                byte [ ] abytFileBytes = File.ReadAllBytes ( cr.strFileName );

                Console.WriteLine (
                    SHA_RPT ,
                    "256" ,
                    DigestString.SHA256Hash ( abytFileBytes ) ,
                    string.Empty );
                Console.WriteLine (
                    SHA_RPT ,
                    "384" ,
                    DigestString.SHA384Hash ( abytFileBytes ) ,
                    string.Empty );
                Console.WriteLine (
                    SHA_RPT ,
                    "512" ,
                    DigestString.SHA512Hash ( abytFileBytes ) ,
                    Environment.NewLine );
            }   // foreach ( string strFileName in rs_astrDigestTestFileNames )

            Console.WriteLine ( EPILOGUE );
        }   // DigestFileTests


        private static bool DoThisTest ( int pintTestNumber )
        {
            if ( s_intNArgs == ArrayInfo.ARRAY_FIRST_ELEMENT )
            {   // No args == run all.
                return true;    // Just do it.
            }   // TRUE (short circuit) block, if ( s_intNArgs == ArrayInfo.ARRAY_FIRST_ELEMENT )
            else
            {
                if ( pintTestNumber < s_intNTests && pintTestNumber > ArrayInfo.ARRAY_INVALID_INDEX )
                {   // Argument pintTestNumber passed the sniff test.
                    int intPgmArgIndex = ArrayInfo.ARRAY_INVALID_INDEX;

                    while ( ++intPgmArgIndex < s_intNArgs )
                    {
                        if ( s_astrPgmArgs [ intPgmArgIndex ] == s_astrTestSelections [ pintTestNumber ] )
                        {
                            return true;    // Found one we like.
                        }   // if ( s_astrPgmArgs [ intPgmArgIndex ] == s_astrTestSelections [ pintTestNumber ] )
                    }   // while ( intPgmArgIndex < s_intNArgs )

                    return false;   // Didn't find one we like.
                }   // TRUE (expected outcome) block, if ( pintTestNumber < s_intNTests && pintTestNumber > ArrayInfo.ARRAY_INVALID_INDEX )
                else
                {   // Argument pintTestNumber is out of range.
                    Console.WriteLine (
						Properties.Resources.ERRMSG_INVALID_TEST_INDEX ,
                        pintTestNumber ,
                        s_intNTests ,
                        Environment.NewLine );
                    return false;
                }   // FALSE (UNexpected outcome) block, if ( pintTestNumber < s_intNTests && pintTestNumber > ArrayInfo.ARRAY_INVALID_INDEX )
            }   // FALSE (selective testing) block, if ( s_intNArgs == ArrayInfo.ARRAY_FIRST_ELEMENT )
        }   // DoThisTest


        private static void InitializeTestSelections ( string [ ] pastrPgmArgs )
        {	// These must be initialized at runtime.
            s_astrPgmArgs = pastrPgmArgs;
            s_intNArgs = pastrPgmArgs.Length;
        }   // InitializeTestSelections


        private static void MaxStringLength_Tests ( )
        {
            int intTestNumber = MagicNumbers.ZERO;

            Console.WriteLine (
                SharedUtl4_TestStand.Properties.Resources.IDS_MAX_STRLEN_BEGIN ,
                Environment.NewLine );

            MaxStringLength_Test_Case ( 
                new MaxStringLength_Tester ( ) , 
                ref intTestNumber );

            Console.WriteLine (
                SharedUtl4_TestStand.Properties.Resources.IDS_MAX_STRLEN_END ,
                Environment.NewLine );
        }   // MaxStringLength_Tests


        private static void MaxStringLength_Test_Case (
            MaxStringLength_Tester pTestKit ,
            ref int pintTestNumber )
        {
            Console.WriteLine (
                SharedUtl4_TestStand.Properties.Resources.IDS_TEST_BEGIN ,
                ++pintTestNumber ,
                Environment.NewLine );

            pTestKit.Go ( );

            Console.WriteLine (
                SharedUtl4_TestStand.Properties.Resources.IDS_TEST_END ,
                pintTestNumber ,
                Environment.NewLine );
        }   // MaxStringLength_Test_Case


        private static void MergeNewItemsIntoArray_Tests ( )
        {
            int intTestNumber = MagicNumbers.ZERO;

            Console.WriteLine (
                SharedUtl4_TestStand.Properties.Resources.IDS_MERGENEWITEMSINTOARRAY_BEGIN ,
                Environment.NewLine );
            string strInitialMasterFile = Path.Combine (
                rs_strDataDirectory ,
                Properties.Settings.Default.MergeNewItemsIntoArray_Master );
            string strOutputFilenmetemplate = Properties.Settings.Default.MergeNewItemsIntoArray_Outputs;

            string strNewItemsListFileSpec = Properties.Settings.Default.MergeNewItemsIntoArray_Cases;
            int intMaxDigitsInCaseNumber = TestCaseMaxDigits ( strNewItemsListFileSpec );
            string strSummaryFileFQFN = Path.Combine (
                rs_strDataDirectory ,
                Properties.Settings.Default.MergeNewItemsIntoArray_Summary );
            string strOutputFileFQFN = null;
            string strInputFileName = null;
            string strReportLabels = MergeNewItemsIntoArray_Tester.ReportLabels.Replace (
				SpecialCharacters.TAB_CHAR ,
                SpecialCharacters.PIPE_CHAR );
            List<string> lstReportDetails = new List<string> ( );
            lstReportDetails.Add ( strReportLabels );

            string strReportDetailTemplate = ReportHelpers.DetailTemplateFromLabels (
                strReportLabels ,
                SpecialCharacters.PIPE_CHAR );
            foreach ( string strNewDataFile in Directory.GetFiles ( rs_strDataDirectory , strNewItemsListFileSpec , SearchOption.TopDirectoryOnly ) )
            {
                strInputFileName = SelectInputFle (
                    intTestNumber ,
                    strInitialMasterFile ,
                    strOutputFileFQFN );
                strOutputFileFQFN = MergeOutputFQFN (
                    strOutputFilenmetemplate ,
                    rs_strDataDirectory ,
                    intMaxDigitsInCaseNumber ,
                    ++intTestNumber );

                MergeNewItemsIntoArray_Tester mergetester = new MergeNewItemsIntoArray_Tester (
                    strInputFileName ,
                    strNewDataFile ,
                    strOutputFileFQFN ,
                    lstReportDetails ,
                    strReportDetailTemplate );
                MergeNewItemsIntoArray_Test_Case (
                    mergetester ,
                    ref intTestNumber );
            }   // foreach ( string strNewDataFile in Directory.GetFiles ( rs_strDataDirectory , strNewItemsListFileSpec , SearchOption.TopDirectoryOnly ) )

            string [ ] astrSummaryReport = new string [ lstReportDetails.Count ];
            lstReportDetails.CopyTo ( astrSummaryReport );
            File.WriteAllLines (
                strSummaryFileFQFN , 
                astrSummaryReport );
            Console.WriteLine (
                SharedUtl4_TestStand.Properties.Resources.IDS_MERGENEWITEMSINTOARRAY_END ,
                Environment.NewLine );
        }   // MergeNewItemsIntoArray_Tests


        private static void MergeNewItemsIntoArray_Test_Case (
            MergeNewItemsIntoArray_Tester pTestKit ,
            ref int pintTestNumber )
        {
            Console.WriteLine (
                SharedUtl4_TestStand.Properties.Resources.IDS_TEST_BEGIN ,
                pintTestNumber ,
                Environment.NewLine );

            pTestKit.Go ( );

            Console.WriteLine (
                SharedUtl4_TestStand.Properties.Resources.IDS_TEST_END ,
                pintTestNumber ,
                Environment.NewLine );
        }   // MergeNewItemsIntoArray_Test_Case


        private static string MergeOutputFQFN ( 
            string pstrNameTemplate ,
            string pstrDataDirectoryName , 
            int pintMaxDigitsInCaseNumber ,
            int pintCaseNumber )
        {
            //  ----------------------------------------------------------------
            //  Construct a fully qualified name from a template like this:
            //
            //     $$DATADIRNAME$$\MergeNewItemsIntoArray_Output_$$CASENBR$$.TXT
            //  ----------------------------------------------------------------

            const string CASE_NUMBER_FORMAT = @"D{0}";
            const string TOKEN_CASENUMBER = @"$$CASENBR$$";
            const string TOKEN_DATADIRNAME = @"$$DATADIRNAME$$";

            string strCaseNumberFormat = string.Format (
                CASE_NUMBER_FORMAT ,
                pintMaxDigitsInCaseNumber );
            string strCaseNumberToken = pintCaseNumber.ToString ( strCaseNumberFormat );
            return pstrNameTemplate.Replace (
                TOKEN_CASENUMBER ,
                strCaseNumberToken ).Replace (
                    TOKEN_DATADIRNAME ,
                    pstrDataDirectoryName );
        }   // MergeOutputFQFN


        private static void ReportDetailsTests ( bool pfTestSorting )
        {
            const uint OUT_OF_ORDER = 900;

            string strTaskName = System.Reflection.MethodBase.GetCurrentMethod ( ).Name;

            Console.WriteLine (
                Properties.Resources.IDS_MSG_BATCH ,
                strTaskName ,
                Properties.Resources.IDS_MSG_BEGIN ,
                Environment.NewLine );

            if ( pfTestSorting )
                FormatStringParsing_Drills.TestFormatItemCounters ( );

            ReportDetails rdColl = new ReportDetails ( );

            if ( pfTestSorting )
                Console.WriteLine (
                    Properties.Resources.MSG_REPORT_DETAILS_SELECTIVELY_OVERRIDDEN ,
                    Environment.NewLine );
            else
                Console.WriteLine (
                    Properties.Resources.MSG_REPORT_DETAILS_AUTO_ORDERED ,
                    Environment.NewLine );

            rdColl.Add ( new ReportDetail (
                Properties.Resources.IDS_MSG_REPORT_LABEL_1 ,
                Properties.Resources.IDS_MSG_REPORT_LABEL_1.Length ,
                Properties.Resources.IDS_MSG_REPORT_LABEL_1.Length.ToString ( ) ) );
            rdColl.Add ( new ReportDetail (
                Properties.Resources.IDS_MSG_REPORT_LABEL_2 ,
                Properties.Resources.IDS_MSG_REPORT_LABEL_2.Length ,
                Properties.Resources.IDS_MSG_REPORT_LABEL_2.Length.ToString ( ) ) );
            rdColl.Add ( new ReportDetail (
                Properties.Resources.IDS_MSG_REPORT_LABEL_3 ,
                Properties.Resources.IDS_MSG_REPORT_LABEL_3.Length ,
                Properties.Resources.IDS_MSG_REPORT_LABEL_3.Length.ToString ( ) ,
                ( ReportDetail.ItemDisplayOrder ) OUT_OF_ORDER ) );                                   // This one is intentionally out of order.
            rdColl.Add ( new ReportDetail (
                Properties.Resources.IDS_MSG_REPORT_LABEL_4 ,
                Properties.Resources.IDS_REALLY_LONG_STRING.Length ) ); // Leave the display value NULL.

            Console.WriteLine (
                Properties.Resources.IDS_MSG_LONGEST_LABEL ,
                rdColl.WidthOfWidestLabel );
            Console.WriteLine (
                Properties.Resources.IDS_MSG_LONGEST_VALUE ,
                rdColl.WidthOfWidestValue ,
                Environment.NewLine );

            if ( pfTestSorting )
            {   // The sorting pass shows it twice.
                Console.WriteLine (
                    Properties.Resources.MSG_REPORT_DETAILS_UNSORTED ,
                    Environment.NewLine );
            }   // if ( pfTestSorting )

            //  ----------------------------------------------------------------
            //  Since the value of this property is computed on demand by
            //  enumerating the collection, it is more efficient to hoist it out
            //  of the loop that also enumerates it. This change improves the
            //  performance of the foreach loop that follows it from an O(n^2)
            //  operation to an O(n) operation.
            //
            //  Since the typical size of these report item collections is on
            //  tho order of a few hundred items or less, the computational 
            //  impact of this change is too small to measure without running
            //  hundreds or thousands of iterations. Nevertheless, when it's so
            //  easy to do so, I believe that any operation should be designed
            //  to scale as well as possible.
            //  ----------------------------------------------------------------

            int intWidthOfWidestLabel = rdColl.WidthOfWidestLabel;
            int intWidthOfWidestValue = rdColl.WidthOfWidestValue;

            foreach ( ReportDetail rdItem in rdColl )
            {
                Console.WriteLine (
                    ReportDetail.DEFAULT_FORMAT ,
                    intWidthOfWidestLabel ,
                    rdItem.GetPaddedValue (
                        intWidthOfWidestValue ,
                        FormatItem.Alignment.Right ,
                        NumericFormats.DECIMAL ) );
            }   // foreach ( ReportDetail rdItem in rdColl )

            if ( pfTestSorting )
            {   // The second pass follows a sort.
                Console.WriteLine (
                    Properties.Resources.MSG_REPORT_DETAILS_UNSORTED ,
                    Environment.NewLine );

                rdColl.Sort ( );    // ReportDetails implements IComparable. Use it.

                foreach ( ReportDetail rdItem in rdColl )
                {
                    Console.WriteLine (
                        ReportDetail.DEFAULT_FORMAT ,
                        rdItem.GetPaddedLabel ( intWidthOfWidestLabel ) ,
                        rdItem.GetPaddedValue (
                            intWidthOfWidestValue ,
                            FormatItem.Alignment.Right ,
                            NumericFormats.DECIMAL ) );
                }   // foreach ( ReportDetail rdItem in rdColl )
            }   // if ( pfTestSorting )

            Console.WriteLine (
                Properties.Resources.IDS_MSG_BATCH ,
                strTaskName ,
                Properties.Resources.IDS_MSG_DONE ,
                Environment.NewLine );
        }   // private void ReportDetailsTests


        private static void ReportHelpersTests ( )
        {
            const uint DESIRED_WIDTH_MINIMUM = 1;
            const uint DESIRED_WIDTH_MAXIMUM = 10;

            const uint ITEM_NUMBER_MINIMUM = 0;
            const uint ITEM_NUMBER_MAXIMUM = 4;

            string strTaskName = System.Reflection.MethodBase.GetCurrentMethod ( ).Name;

            Console.WriteLine (
                Properties.Resources.IDS_MSG_BATCH ,
                strTaskName ,
                Properties.Resources.IDS_MSG_BEGIN ,
                Environment.NewLine );

            uint uintTestNumber = MagicNumbers.ZERO;
            uint uintExceptionCount = MagicNumbers.ZERO;
            long lngTotalTests = MagicNumbers.ZERO;
            string strHeadingFormat = null;

#if DBG_REPORTHELPERSTESTS
            uint uintItemIndexCounter = MagicNumbers.ZERO;
#endif  // DBG_REPORTHELPERSTESTS

            for ( uint uintItemIndex = ITEM_NUMBER_MINIMUM ;
                       uintItemIndex <= ITEM_NUMBER_MAXIMUM ;
                       uintItemIndex++ )                           
            {

#if DBG_REPORTHELPERSTESTS
                Console.WriteLine (
                    "LOOP DEBUG: uintItemIndexCounter = {0}, uintItemIndex = {1}" ,
                    ++uintItemIndexCounter ,
                    uintItemIndex );
                uint uintMaximumWidthCounter = MagicNumbers.ZERO;
#endif  // DBG_REPORTHELPERSTESTS

                for ( uint uintMaximumWidth = DESIRED_WIDTH_MINIMUM ;
                           uintMaximumWidth < DESIRED_WIDTH_MAXIMUM ;
                           uintMaximumWidth++ )
                {

#if DBG_REPORTHELPERSTESTS
                    Console.WriteLine (
                        "LOOP DEBUG: uintMaximumWidthCounter = {0}, uintMaximumWidth = {1}" ,
                        ++uintMaximumWidthCounter ,
                        uintMaximumWidth );
                    uint uintAlignmentCounter = MagicNumbers.ZERO;
#endif  // DBG_REPORTHELPERSTESTS

                    foreach ( FormatItem.Alignment enmAlignment in s_aenmAlignment )
                    {

#if DBG_REPORTHELPERSTESTS
                        Console.WriteLine (
                            "LOOP DEBUG: uintAlignmentCounter = {0}, enmAlignment = {1}" ,
                            ++uintAlignmentCounter ,
                            enmAlignment );
                        uint uintTestFormatCounter = MagicNumbers.ZERO;
#endif  // DBG_REPORTHELPERSTESTS

                        foreach ( string strTestFormat in s_astrFormats )
                        {

#if DBG_REPORTHELPERSTESTS
                        Console.WriteLine (
                            "LOOP DEBUG: uintTestFormatCounter = {0}, strTestFormat = {1}" ,
                            ++uintTestFormatCounter ,
                            strTestFormat );
#endif  // DBG_REPORTHELPERSTESTS

                            if ( lngTotalTests == MagicNumbers.ZERO )
                            {
								lngTotalTests = ( ( ITEM_NUMBER_MAXIMUM - ITEM_NUMBER_MINIMUM ) + ArrayInfo.ORDINAL_FROM_INDEX )
                                                * ( DESIRED_WIDTH_MAXIMUM - DESIRED_WIDTH_MINIMUM )
                                                * s_aenmAlignment.Length
                                                * s_astrFormats.Length;
                                strHeadingFormat = FormatItem.UpgradeFormatItem (
                                    Properties.Resources.MSG_NEW_TEST ,
                                    ITEM_NUMBER_MINIMUM ,
                                    FormatItem.AdjustToMaximumWidth (
                                        ITEM_NUMBER_MINIMUM ,
                                        ( uint ) lngTotalTests.ToString (
                                            NumericFormats.NUMBER_PER_REG_SETTINGS_0D ).Length ,
                                        FormatItem.Alignment.Right ,
                                        NumericFormats.NUMBER_PER_REG_SETTINGS_0D ) );
                            }   // if ( lngTotalTests == MagicNumbers.ZERO )

                            Console.WriteLine (
                                strHeadingFormat ,
                                ++uintTestNumber ,
                                lngTotalTests ,
                                Environment.NewLine );
                            string strUpgradedFormat = FormatItem.AdjustToMaximumWidth (
                                uintItemIndex ,
                                uintMaximumWidth ,
                                enmAlignment ,
                                strTestFormat );

                            ReportDetails rdColl = new ReportDetails ( );

                            rdColl.Add ( new ReportDetail (
                                Properties.Resources.MSG_PENMALIGNMENT ,
                                ( object ) uintItemIndex ) );
                            rdColl.Add ( new ReportDetail (
                                Properties.Resources.MSG_PUINTMAXIMUMWIDTH ,
                                ( object ) uintMaximumWidth ) );
                            rdColl.Add ( new ReportDetail (
                                Properties.Resources.MSG_PENMALIGNMENT ,
                                enmAlignment ) );
                            rdColl.Add ( new ReportDetail (
                                Properties.Resources.MSG_PSTRFORMATSTRING ,
                                ( object ) strTestFormat ) );               // Coerce strTestFormat into the object.

                            rdColl.Add ( new ReportDetail (
                                Properties.Resources.MSG_UPGRADED_FORMAT ,
                                ( object ) strUpgradedFormat ) );

                            int uintRequiredWidth = rdColl.WidthOfWidestLabel;

                            foreach ( ReportDetail rdItem in rdColl )
                            {
                                Console.WriteLine (
                                    ReportDetail.DEFAULT_FORMAT ,
                                    rdItem.Label.PadRight ( ( int ) uintRequiredWidth ) ,
                                    rdItem.Value );
                            }   // foreach ( ReportDetail rdItem in rdColl )


                            //  ------------------------------------------------
                            //  There are intentionally programed exceptions in
                            //  this test.
                            //  ------------------------------------------------

                            try
                            {
                                Console.WriteLine (
                                    Properties.Resources.MSG_SHOW_SAMPLE_BEFORE_AND_AFTER ,
                                    Properties.Resources.MSG_SAMPLE_FORMAT_STRING ,
                                    FormatItem.UpgradeFormatItem (
                                        Properties.Resources.MSG_SAMPLE_FORMAT_STRING ,
                                        uintItemIndex ,
                                        strUpgradedFormat ) ,
                                    Environment.NewLine );
                            }
                            catch ( Exception exAllKinds )
                            {
								rs_appThis.BaseStateManager.AppExceptionLogger.ReportException ( exAllKinds );
                                uintExceptionCount++;
                            }

                            if ( uintTestNumber == lngTotalTests )
                            {   // We need just one test of this type of error.
                                try
                                {
                                    Console.WriteLine (
                                        Properties.Resources.MSG_SHOW_SAMPLE_BEFORE_AND_AFTER ,
                                        Properties.Resources.MSG_SAMPLE_FORMAT_STRING ,
                                        FormatItem.UpgradeFormatItem (
                                            Properties.Resources.MSG_SAMPLE_FORMAT_STRING ,
                                            uintItemIndex ,
                                            "(4,9:X8)" ) ,      // This format item is invalid.
                                        Environment.NewLine );
                                }
                                catch ( Exception exAllKinds )
                                {
									rs_appThis.BaseStateManager.AppExceptionLogger.ReportException ( exAllKinds );
                                    uintExceptionCount++;
                                }
                            }   // if ( uintTestNumber == lngTotalTests )
                        }   // foreach ( string strTestFormat in s_astrFormats )
                    }   // foreach ( FormatItem.Alignment enmAlignment in s_aenmAlignment )
                }   // for ( uint uintMaximumWidth = MINIMUM_DESIRED_WIDTH ; uintMaximumWidth <= MAXIMUM_DESIRED_WIDTH ; uintMaximumWidth++ )
            }   // for ( uint uintItemIndex = MINIMUM_ITEM_NUMBER ; uintItemIndex < FORMAT_ITEM_LIMIT ; uintItemIndex++ )

            Console.WriteLine (
                Properties.Resources.MSG_EXCETIONS_COUNTED ,
                uintExceptionCount ,
                Environment.NewLine );
            Console.WriteLine (
                Properties.Resources.IDS_MSG_BATCH ,
                strTaskName ,
                Properties.Resources.IDS_MSG_DONE ,
                Environment.NewLine );
        }   // ReportHelpersTests


        private static string SelectInputFle (
            int pintTestNumber ,
            string pstrInitialMasterFile , 
            string pstrOutputFileFQFN )
        {
            if ( pintTestNumber == MagicNumbers.ZERO )
                return pstrInitialMasterFile;   // First pas uses this, and no output file has yet been named.
            else
                return pstrOutputFileFQFN;      // Since this function is called first, this is the output from the previous loop.
        }   // SelectInputFle


        private static int TestCaseMaxDigits ( string pstrNewItemsListFileSpec )
        {
            const char FILEMASK_WILD_ONE = '?';

            int rintMaxDigits = MagicNumbers.ZERO;

            foreach ( char chr in pstrNewItemsListFileSpec.ToCharArray ( ) )
                if ( chr == FILEMASK_WILD_ONE )
                    rintMaxDigits++;

            return rintMaxDigits;
        }   // TestCaseMaxDigits
    }   // class Program
}   // partial namespace SharedUtl4_TestStand