FROM node:18 as builder
WORKDIR /app

COPY /package*.json ./

RUN npm install

COPY ./ ./


# Expose port
EXPOSE 3000

# Start
CMD ["node", "app.js" ]
