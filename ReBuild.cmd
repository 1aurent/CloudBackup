ECHO OFF
MODE CON COLS=1024 LINES=4096
ECHO "Project WindowsRRD - Full Build"
CALL "%VS120COMNTOOLS%\vsvars32.bat"
MSBUILD CloudBackup.Build
IF NOT "%ERRORLEVEL%" EQU "0" GOTO FailedFailed

ECHO #######           ###
ECHO #     #  #    #   ###
ECHO #     #  #   #    ###
ECHO #     #  ####      #
ECHO #     #  #  #
ECHO #     #  #   #    ###
ECHO #######  #    #   ###
sleep 10
Goto Fin

:FailedFailed
ECHO #######    #      ###   #       ####### ######    ###
ECHO #         # #      #    #       #       #     #   ###
ECHO #        #   #     #    #       #       #     #   ###
ECHO #####   #     #    #    #       #####   #     #    #
ECHO #       #######    #    #       #       #     #
ECHO #       #     #    #    #       #       #     #   ###
ECHO #       #     #   ###   ####### ####### ######    ###
PAUSE

:Fin
ECHO (End)