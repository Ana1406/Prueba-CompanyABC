# --- Stage 1: Build ---
FROM node:20-alpine AS build

# Definir el directorio de trabajo
WORKDIR /app

COPY package*.json ./

# Instalar dependencias (usando npm ci para builds limpios y rápidos)
RUN npm ci

COPY . .

# Generar el build de producción
RUN npm run build -- --configuration production

# --- Stage 2: Serve ---
FROM nginx:stable-alpine

RUN rm -rf /usr/share/nginx/html/*

COPY --from=build /app/dist/company-ABC/browser /usr/share/nginx/html


EXPOSE 80

CMD ["nginx", "-g", "daemon off;"]