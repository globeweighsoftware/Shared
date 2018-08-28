REG ADD HKLM\SYSTEM\CurrentControlSet\Services\RapiMgr /v SvcHostSplitDisable /t REG_DWORD /d 1 /f
REG ADD HKLM\SYSTEM\CurrentControlSet\Services\WcesComm /v SvcHostSplitDisable /t REG_DWORD /d 1 /f
pause