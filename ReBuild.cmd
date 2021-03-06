ECHO OFF
MODE CON COLS=1024 LINES=4096

ECHO               ,,                           ,,                                                                                                   ,,    ,,        ,,  
ECHO   .g8"""bgd `7MM                         `7MM  `7MM"""Yp,                `7MM                                          `7MM"""Yp,               db  `7MM      `7MM  
ECHO .dP'     `M   MM                           MM    MM    Yb                  MM                                            MM    Yb                     MM        MM  
ECHO dM'       `   MM  ,pW"Wq.`7MM  `7MM   ,M""bMM    MM    dP  ,6"Yb.  ,p6"bo  MM  ,MP'`7MM  `7MM `7MMpdMAo.                 MM    dP `7MM  `7MM  `7MM    MM   ,M""bMM  
ECHO MM            MM 6W'   `Wb MM    MM ,AP    MM    MM"""bg. 8)   MM 6M'  OO  MM ;Y     MM    MM   MM   `Wb                 MM"""bg.   MM    MM    MM    MM ,AP    MM  
ECHO MM.           MM 8M     M8 MM    MM 8MI    MM    MM    `Y  ,pm9MM 8M       MM;Mm     MM    MM   MM    M8     mmmmm       MM    `Y   MM    MM    MM    MM 8MI    MM  
ECHO `Mb.     ,'   MM YA.   ,A9 MM    MM `Mb    MM    MM    ,9 8M   MM YM.    , MM `Mb.   MM    MM   MM   ,AP                 MM    ,9   MM    MM    MM    MM `Mb    MM  
ECHO   `"bmmmd'  .JMML.`Ybmd9'  `Mbod"YML.`Wbmd"MML..JMMmmmd9  `Moo9^Yo.YMbmd'.JMML. YA.  `Mbod"YML. MMbmmd'                .JMMmmmd9    `Mbod"YML..JMML..JMML.`Wbmd"MML.
ECHO                                                                                                 MM                                                                  
ECHO                                                                                               .JMML.                                                                


IF EXIST "%ProgramFiles(x86)%\MSBuild\12.0\Bin\MSBuild.exe" GOTO BuildMe

ECHO .. MSBUILD not detected or unsupported 32bit machine ..
ECHO Ensure VS2013 and Windows 64bit
GOTO Fin


:BuildMe
"%ProgramFiles(x86)%\MSBuild\12.0\Bin\MSBuild.exe" CloudBackup.Build
IF NOT "%ERRORLEVEL%" EQU "0" GOTO FailedFailed


ECHO   _____   __    __     ____     ____    _____    _____    _____ 
ECHO  / ____\  ) )  ( (    / ___)   / ___)  / ___/   / ____\  / ____\
ECHO ( (___   ( (    ) )  / /      / /     ( (__    ( (___   ( (___  
ECHO  \___ \   ) )  ( (  ( (      ( (       ) __)    \___ \   \___ \ 
ECHO      ) ) ( (    ) ) ( (      ( (      ( (           ) )      ) )
ECHO  ___/ /   ) \__/ (   \ \___   \ \___   \ \___   ___/ /   ___/ / 
ECHO /____/    \______/    \____)   \____)   \____\ /____/   /____/  
                                                                

PAUSE
Goto Fin

:NoVisualStudio
ECHO !!!
ECHO !!! VISUAL STUDIO 2013 IS NOT INSTALLED. UNABLE TO COMPILE
ECHO !!!
PAUSE
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