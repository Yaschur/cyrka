### Build stage
FROM node:alpine as build
COPY package.json package-lock.json ./
RUN npm install && mkdir /cyrka.front && cp -R ./node_modules ./cyrka.front
WORKDIR /cyrka.front
COPY . .
RUN $(npm bin)/ng build --prod

### Test stage

### Deploy stage
FROM nginx:alpine as deploy
COPY nginx/default.conf /etc/nginx/conf.d/
RUN rm -rf /usr/share/nginx/html/*
COPY --from=build /cyrka.front/dist /usr/share/nginx/html
CMD ["nginx", "-g", "daemon off;"]
