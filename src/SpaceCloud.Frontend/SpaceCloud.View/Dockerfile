FROM node:lts-alpine as ui-builder
RUN mkdir app
WORKDIR app
ENV PATH /app/node_modules/.bin:$PATH
COPY package.json /app/package.json


RUN npm install
RUN npm install -g @vue/cli
 

COPY . /app
RUN npm run build
 
FROM nginx:latest as production-stage
RUN mkdir /app
COPY  --from=ui-builder /app/dist /app
EXPOSE 80
EXPOSE 443