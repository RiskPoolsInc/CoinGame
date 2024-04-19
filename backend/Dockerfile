FROM node:18 as builder
WORKDIR /app

COPY /package*.json /app/

RUN npm install

COPY ./ /app/


# Expose port
EXPOSE 3000

# Start
CMD ["node", "app.js" ]
