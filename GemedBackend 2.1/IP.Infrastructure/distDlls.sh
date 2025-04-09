#!/bin/bash

# Defina as variáveis necessárias
SOLUTION_PATH="./IP.Infrastructure.sln"
BUILD_CONFIGURATION="Release"
BASE_OUTPUT_DIR="/Infra/IP.Dlls.Infrastructure"

# Lista de diretórios de saída
OUTPUT_DIRS=(
"../IP.Usuario/Infrastructure/IP.Dlls.Infrastructure"
"../IP.Seguranca/Infrastructure/IP.Dlls.Infrastructure"
"../IP.ManipulacaoMedicamentos/Infrastructure/IP.Dlls.Infrastructure"
"../IP.Financeiro/Infrastructure/IP.Dlls.Infrastructure"
"../IP.Faturamento/Infrastructure/IP.Dlls.Infrastructure"
"../IP.EstoqueCompras/Infrastructure/IP.Dlls.Infrastructure"
"../IP.Assistencial/Infrastructure/IP.Dlls.Infrastructure"
"../IP.Agendamento/Infrastructure/IP.Dlls.Infrastructure"
"../IP.Administrativo/Infrastructure/IP.Dlls.Infrastructure"
"../IP.Gateway/Infrastructure/IP.Dlls.Infrastructure"
"../IP.Clientes/Infrastructure/IP.Dlls.Infrastructure")

# Compilação da solução
dotnet build "$SOLUTION_PATH" -c "$BUILD_CONFIGURATION"

# Encontra todos os diretórios bin para cada projeto e copia as DLLs
for project_dir in $(find . -type d -name bin); do
    project_name=$(basename $(dirname "$project_dir"))
    # Copia as DLLs para cada diretório de saída
    for output_dir in "${OUTPUT_DIRS[@]}"; do
        cp "$project_dir/$BUILD_CONFIGURATION/net7.0/$project_name.dll" "$output_dir/$project_name.dll"
    done
done
