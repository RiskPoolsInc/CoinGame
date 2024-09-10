# Create image based on the Skaffolder Node ES6 image
FROM node:20 as builder

WORKDIR /build

COPY package*.json /build/

RUN npm install


COPY ./ /build/


# Build
RUN npm run build

# ----------------------------------
# Prepare production environment
FROM nginx:alpine
# ----------------------------------

# Clean nginx
RUN rm -rf /usr/share/nginx/html/*

# Copy dist
COPY --from=builder /build/dist /usr/share/nginx/html
COPY docker/nginx.conf /etc/nginx/nginx.conf
#COPY docker/99-url-replacement.sh /docker-entrypoint.d/99-url-replacement.sh
#COPY nginx.conf /etc/nginx/nginx.conf

WORKDIR /usr/share/nginx/html

# Permission
RUN chown root /usr/share/nginx/html/*
RUN chmod 755 /usr/share/nginx/html/*

RUN ls -a

# Expose port
EXPOSE 2000

# Start
CMD ["nginx", "-g", "daemon off;"]
