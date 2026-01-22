#!/bin/bash
cd /home/kavia/workspace/code-generation/customer-management-proof-of-concept-26451-26461/customer_management_backend
dotnet build --no-restore -v quiet -nologo -consoleloggerparameters:NoSummary /p:TreatWarningsAsErrors=false
LINT_EXIT_CODE=$?
if [ $LINT_EXIT_CODE -ne 0 ]; then
  exit 1
fi

