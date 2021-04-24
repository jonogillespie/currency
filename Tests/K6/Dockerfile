FROM loadimpact/k6 as BUILD

ARG ENVIRONMENT

COPY ["Scripts", "Scripts"]

RUN k6 run Scripts/$ENVIRONMENT.js