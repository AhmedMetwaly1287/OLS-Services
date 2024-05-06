# Use a lighter base image
FROM node:14-slim

# Set working directory
WORKDIR /app

# Copy package.json and package-lock.json
COPY package*.json ./

# Install dependencies
RUN npm install --production

# Copy built files
COPY --from=build /app/build ./build

# Expose port
EXPOSE 3000

# Command to run the application
CMD ["npm", "start"]
