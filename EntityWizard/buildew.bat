@ECHO OFF

CLS

ECHO ***************************************************
ECHO ***      Titan wizards for Sage CRM 6 & 7       ***
ECHO ***************************************************

SET SCPATH=%THISPATH%
SET CODEPATH=%SCPATH%

SET TITANEW=D:\ats\CRM\SourceControl\dotnet\EntityWizard

SET V7WIZFOLDER=%TITANEW%\TitanCRMEW7\
SET V6WIZFOLDER=%TITANEW%\TitanCRMEntityWizard\

SET V7WIZDEST="%V7WIZFOLDER%\TitanCRMEW7\"
SET V6WIZDEST="%V6WIZFOLDER%\TitanEntityWizard\"

SET V7WIZDESTCP="%V7WIZDEST%\SageCRM\component"
SET V6WIZDESTCP="%V6WIZDEST%\SageCRM\component"

SET V7WIZDESTCPBIN="%V7WIZDEST%\custompages\entwiz\Bin"
SET V6WIZDESTCPBIN="%V6WIZDEST%\custompages\entwiz\Bin"

SET COMPONENT="D:\ats\CRM\SourceControl\dotnet\CustomPages\SageCRM\component"
SET SAGECRMDLL="D:\ats\CRM\SourceControl\dotnet\SageCRM\SageCRM\bin\Release\SageCRM_Secure\SageCRM.dll" 

ECHO Delete old custom pages
DEL %V7WIZDESTCP%\*.*
DEL %V6WIZDESTCP%\*.*

REM Copy latest Sage CRM custom pages files
ECHO Copy latest custom pages
COPY %COMPONENT%\*.* %V7WIZDESTCP%
COPY %COMPONENT%\*.* %V6WIZDESTCP%

REM Copy latest dll build into "custompages\entwiz\Bin" folder
COPY %SAGECRMDLL% %V7WIZDESTCPBIN%
COPY %SAGECRMDLL% %V6WIZDESTCPBIN%

REM DEL %V7WIZFOLDER%\TitanCRMEW7.zip
REM DEL %V6WIZFOLDER%\TitanCRMEW7.zip

REM cd %TITANEW%
REM ZIP -r  %TITANEW%\TitanCRMEW7.zip  %V7WIZFOLDER%\*.*
REM ZIP -r  %TITANEW%\TitanCRMEntityWizard.zip  %V6WIZFOLDER%\*.*

:END