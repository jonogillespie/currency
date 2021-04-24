FROM postman/newman as BUILD

ARG ENVIRONMENT

COPY ["Scripts", "Scripts"]
COPY ["Environments", "Environments"]

RUN newman run Scripts/postman.json \
                --environment Environments/$ENVIRONMENT.json \
                --reporters cli \