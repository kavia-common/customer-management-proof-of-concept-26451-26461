#!/bin/bash
cd /home/kavia/workspace/code-generation/customer-management-proof-of-concept-26451-26461/customer_management_backend

# Restore is required in CI so project.assets.json exists for all projects in the solution.
dotnet restore CustomerManagement.sln -v quiet -nologo
RESTORE_EXIT_CODE=$?
if [ $RESTORE_EXIT_CODE -ne 0 ]; then
  exit 1
fi

# Explicitly build the solution to avoid MSBuild ambiguity when multiple projects exist in the folder.
dotnet build CustomerManagement.sln --no-restore -v quiet -nologo -consoleloggerparameters:NoSummary /p:TreatWarningsAsErrors=false
LINT_EXIT_CODE=$?
if [ $LINT_EXIT_CODE -ne 0 ]; then
  exit 1
fi

