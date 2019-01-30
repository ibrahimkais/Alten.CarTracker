# stage 1
FROM node:latest as node
WORKDIR /app
COPY Alten.CarTracker.FrontEnd.UI/ .
RUN yarn install --network-timeout 300000
RUN yarn add node-sass
RUN yarn run build --prod

# stage 2
FROM nginx:alpine
COPY --from=node /app/dist/carApp /usr/share/nginx/html