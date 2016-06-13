@echo off
goto SKIPREM

::  ============================================================================
::
::  Name:           RunAllUnitTests.CMD
::
::  Synopsis:       Execute the unit test suite that covers the following small
::                  class libraries that were developed and tested together,
::                  since the third depends heavily on the first two.
::
::                          WizardWrx.ASCIIInfo.dll
::                          WizardWrx.FormatStringEngine.dll
::                          WizardWrx.SharedUtl4.dll
::
::  Arguments:      %1 = One of the following, to designate which build to run:
::
::                          ----------------------------------------------------
::                          Value   Interpretation
::                          ------- --------------------------------------------
::                          D       Use the Debug build to run the unit tests.
::                          Deb     Use the Debug build to run the unit tests.
::                          Debug   Use the Debug build to run the unit tests.
::
::                          R       Use the Release build to run the unit tests.
::                          Rel     Use the Release build to run the unit tests.
::                          Release Use the Release build to run the unit tests.
:;                          Ret     Use the Release build to run the unit tests.
::                          Retail  Use the Release build to run the unit tests.
::                          ----------------------------------------------------
::
::                  %2 = One of the following, to designate which test to run:
::
::                          -------------------------------------------------------------------------
::                          Value                   Run this unit test routine.
::                          ----------------------  -------------------------------------------------
::                          MaxStringLength         MaxStringLength_Tests
::                          MergeNewItemsIntoArray  MergeNewItemsIntoArray_Tests
::                          DigestFile              DigestFileTests
::                          ReportDetails           ReportDetailsTests
::                          ReportHelpers           ReportHelpersTests
::                          FormatItemGen           FormatStringParsing_Drills.TestFormatItemBuilders
::                          ASCIITableGen           FormatStringParsing_Drills.ASCII_Table_Gen
::                          -------------------------------------------------------------------------
::
::  Dependencies:   Date2FN.exe   = Append the Last Write time stamp to the base
::                                  name of a file.
::
::                  WWLOGGER.EXE  = Make a permanent record of all invocations,
::                                  recorded in a log file that sits alongside
::                                  the script (in the same directory, same base
::                                  name, but different extension.
::
::                  WWPause.exe   = Halt the script pending acknowledgement via
::                                  Enter key by a carbon unit.
::
::                  Both dependencies must be installed into a directory that is
::                  in the Windows PATH list.
::
::  Remarks:        Strictly speaking, WWPause.exe is optional, since all you
::                  lose if it is absent is a halt, awaiting a carbon unit to
::                  press the Enter key at the end of the script.
::
::                  However, without Date2FN.exe, you lose a permanent record of
::                  the details of the operation, since the output log file is
::                  overwritten by the next invocation.
::
::  ----------------------------------------------------------------------------
::  Revision History
::  ----------------------------------------------------------------------------
::
::  Date       By  Synopsis
::  ---------- --- -------------------------------------------------------------
::  2016/06/11 DAG Adapt LoggedShellScript.CMD to create the initial version.
::  ============================================================================


::  ----------------------------------------------------------------------------
::  Since I like to keep the headnote with the routine that it deocuments, I put
::  it below the label that denotes its start, and immediately jump over it, to
::  keep the comments, themselves, out of the execution path. This is the result
::  of hard won knowledtge about the strange things that can hapen when you have
::  inline comments, especially when a stray character finds its way into the
::  comment.
::  ----------------------------------------------------------------------------

:LOG_ITEM

    goto LOG_ITM1

::  ----------------------------------------------------------------------------
::  Subroutine Name:    LOG_ITEM
::
::  Synopsis:           Append a message to a specified time stamped log file,
::                      with optional appending to a second freeform text file.
::
::  Arguments:          %1 = Time stamped log file maintained by WWLOGGER.EXE
::
::                      %2 = Optional freeform log file, intended to capture all
::                           output from a script, such as directory listings or
::                           general program output
::
::                      %3 = Message to append to one or both log files.
::
::  Remarks:            The first of the two output files named gets a copy of
::                      the message in the last argument (the second of two or
::                      the third of three), preceded by a time stamp and one
::                      space character. If specified, the second file is plain
::                      text, which gets the text exactly as input, preceded and
::                      followed by one blank line.
::
::                      If you want both outputs, you must specify two separate
::                      files, followed by the message. The same message boes in
::                      both files. Otherwise, specify one file only followed by
::                      the message.
::
::                      The message must be enclosed in double quotation marks
::                      if it contains embedded spaces. The same goes for both
::                      log file names.
::
::                      IMPORTANT:  When the message is eachoed to the freeform
::                                  file, it must be UNquoted, because the ECHO
::                                  command is absolutely literal. On the other
::                                  hand, passing the message to WWLOGGER.EXE
::                                  requires the quotation marks, since the
::                                  command parser strips them when constructing
::                                  its command tail.
::
::                      Finally, :EOF is a reserved magic label that exits the
::                      current block. When the current block is a script, the
::                      entire script ends, but when the current block is a
::                      subroutine, the subroutine exits, and control returns to
::                      the calling script, whether that happens to be elsewhere
::                      in the same script file or in a different script file.
::  ----------------------------------------------------------------------------

:LOG_ITM1

    if !%LOG_ITM1%! equ !DEBUG! (
        echo.
        echo ------------------------------------------------------------------------------
        echo LOG_ITM1: Argument 1 = %1
        echo LOG_ITM1: Argument 2 = %2
        echo LOG_ITM1: Argument 3 = %3
        echo ------------------------------------------------------------------------------
        echo.
    )

    if !%3! equ !! (
        echo.
        WWLOGGER.EXE "%~1" "%~2"
        echo.
    ) else (
        echo.
        WWLOGGER.EXE "%~1" "%~3"
        echo.
        echo.    >> "%~2"
        echo %~3 >> "%~2"
        echo.    >> "%~2"
    )

    goto :EOF

::  +--------------------------------------------------------------------------+
::  |                   B e g i n   M a i n   R o u t i n e                    |
::  +--------------------------------------------------------------------------+


:SKIPREM

    goto SKIPRM1

::  ----------------------------------------------------------------------------
::  Display a startup message, then verify that command extensions are enabled.
::
::  NOTE:   On Wednesday, 05 February 2014, I discovered that the test below is
::          broken, but the outcome is more or less acceptable, because it kills
::          the script immediately.
::
::  Moreover, there are circumstances in which %~t0 is not interpolated, either,
::  but it is replaced by the modified date of the executing script under most
::  circumstances, even when the script is called from a secondary shell, even
::  when that shell happens to be COMMAND.COM.
::  ----------------------------------------------------------------------------

:SKIPRM1

    echo.
    WWLOGGER.EXE "%~dpn0.LOG" BOJ %0, version %~t0
    echo.

    if CMDEXTVERSION 1  goto ARBEITE

    echo.
    echo ----------------------------------------------------
    echo     This script requires command extensions to be enabled. Since they
    echo     are enabled, by default, they have been disabled, directly or by GPO.

    goto ERR_DONE

::  ----------------------------------------------------------------------------
::  Though it is technically legal to allow the command processor to parse these
::  comments, which it should skip, I have learned the hard way that doing so is
::  unwise, because it takes only one illegal character such as an angle bracket
::  in just the right spot to make a mess of your careful work, with output that
::  makes the cause hard to pin down.
::  ----------------------------------------------------------------------------

:ARBEITE

    setlocal
    goto ARBEITE1

::  --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
::  Sample log entry:
::
::    call :LOG_ITEM "%~dpn0.LOG" "%~dpn0_OUTPUT.LOG" "Step 1 of 4: Copy C:\Users\DAVE\Documents\Programming\Visual_Studio_6\DLL\ProcessInfo\ProcessInfo32\P6CutilLib1_Source_Repository\* to C:\Users\DAVE\Documents\Programming\Visual_Studio_6\DLL\P6CUtilLib1"
::  --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

:ARBEITE1

    call :LOG_ITEM "%~dpn0.LOG" "%~dpn0_OUTPUT.LOG" "Step 1 of 5: Establish the data directory as the current working directory."

    goto ARBEITE2

::  ----------------------------------------------------------------------------
::  This step uses established, but not terribly well publicized, script name
::  parsing features implemented in the newer command shell, CMD.EXE, many of
::  which work in CODMMAND.COM (on Windows XP and 32 bit versions of Windows 7),
::  too.
::
::  1)  Set the working directory to the directory from which the shell script
::      loaded. Using pushd creates a "stack" of working direcotry names that is
::      unwound as needeb by way of its inverse command, popd. Pushd and Popd
::      are internal commands, implemented directly by the command processor.
::
::  2)  Making the Test_Data directory the working directory is trivial, since
::      it is a sibling of the script directory.
::
::  Since SETLOCAL established a local scope scope, the stack is unwound by the
::  implicit ENDLOCAL implemented when the script ends, and the accompanying
::  destruction of the stack of working directories, which is, effectively, a
:;  locally scoped variable.
::
::  Since environment variable %CD% tracks the current working directory, the
::  outcome of these two commands is simple to record in the script log.
::  ----------------------------------------------------------------------------

:ARBEITE2

    pushd "%~dp0"
    pushd ..\Test_Data

    call :LOG_ITEM "%~dpn0.LOG" "%~dpn0_OUTPUT.LOG" "Step 2 of 5: Set the working directory to %CD%"

    call :LOG_ITEM "%~dpn0.LOG" "%~dpn0_OUTPUT.LOG" "Step 3 of 5 Begin: Decide which build to execute."

    if "%1" equ "" (
        set TEST_BIN=Debug
        goto ARBEITE3
    )

    if /i "%1" equ "d" (
        set TEST_BIN=Debug
        goto ARBEITE3
    )

    if /i "%1" equ "deb" (
        set TEST_BIN=Debug
        goto ARBEITE3
    )

    if /i "%1" equ "debug" (
        set TEST_BIN=Debug
        goto ARBEITE3
    )

    if /i "%1" equ "r" (
        set TEST_BIN=Release
        goto ARBEITE3
    )

    if /i "%1" equ "rel" (
        set TEST_BIN=Release
        goto ARBEITE3
    )

    if /i "%1" equ "release" (
        set TEST_BIN=Release
        goto ARBEITE3
    )

    if /i "%1" equ "ret" (
        set TEST_BIN=Release
        goto ARBEITE3
    )

    if /i "%1" equ "retail" (
        set TEST_BIN=Release
        goto ARBEITE3
    )

    set TEST_BIN=Debug

:ARBEITE3

    call :LOG_ITEM "%~dpn0.LOG" "%~dpn0_OUTPUT.LOG" "Step 3 of 5 Done: Build = %TEST_BIN%."

:ARBEITE4

    call :LOG_ITEM "%~dpn0.LOG" "%~dpn0_OUTPUT.LOG" "Step 4 of 5: ..\SharedUtl4_TestStand\bin\%TEST_BIN%\SharedUtl4_TestStand.exe"

    ..\SharedUtl4_TestStand\bin\%TEST_BIN%\SharedUtl4_TestStand.exe %2 >> "%~dpn0_OUTPUT.LOG"

    call :LOG_ITEM "%~dpn0.LOG" "%~dpn0_OUTPUT.LOG" "Step 4 of 5: Done!"

    goto ARBEITE5

::  ----------------------------------------------------------------------------
::  This step keeps the program output from each invocation of the script in its
::  own log file, while preserving a single time stamped log file that records a
::  history of all invocations in a convenient location, alongside the script,
::  itself.
::
::  Note:   Replace "Step N of N" with an appropriate phrase indicative of the
::          number of steps in your script.
::  ----------------------------------------------------------------------------

:ARBEITE5

    call :LOG_ITEM "%~dpn0.LOG" "%~dpn0_OUTPUT.LOG" "Step 5 of 5: Save the output file into which this record is being written by appending a time stamp to its base name."

    Date2FN.exe -r "%~dpn0_OUTPUT.LOG"

:REG_DONE

    echo.
    WWLOGGER.EXE "%~dpn0.LOG" EOJ %~nx0
    echo.
    goto EOJ_STOP

:ERR_DONE

    echo.
    WWLOGGER.EXE "%~dpn0.LOG" EOJ (ERROR, Command Extensions are off.) %~nx0
    echo.

:EOJ_STOP

    WWPause.exe
