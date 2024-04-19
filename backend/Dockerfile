FROM node:18 as builder
WORKDIR /proxy

COPY /backend/package*.json /proxy/

RUN npm install

COPY ./backend/ /backend/


# Expose port
EXPOSE 3000

# Start
CMD ["node", "app.js" ]
