# Use a imagem oficial do MongoDB como base
FROM mongo

# Define o hostname usando a instrução LABEL
LABEL HOSTNAME=mongodbCache

# Configurações do ambiente para o MongoDB
ENV MONGO_INITDB_ROOT_USERNAME=Gemed2022
ENV MONGO_INITDB_ROOT_PASSWORD=Gemed2022Teste
ENV MONGO_INITDB_DATABASE=dbCache

# Expor a porta padrão do MongoDB
EXPOSE 27017

# Comando de inicialização
CMD ["mongod"]
