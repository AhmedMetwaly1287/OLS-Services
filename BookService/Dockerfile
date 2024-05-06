# Use the official Maven image for building
FROM maven:3.8.5-openjdk-17 AS maven_build

# Set the working directory
WORKDIR /tmp/

# Copy the project files
COPY pom.xml .
COPY src/ /tmp/src/

# Build the project
RUN mvn clean package

# Use the official OpenJDK 17 image for runtime
FROM openjdk:17-jdk-slim

# Set the working directory
WORKDIR /app

# Copy the built JAR file from the maven_build stage
COPY --from=maven_build /tmp/target/BookService-0.0.1-SNAPSHOT.jar .

# Expose port 6010
EXPOSE 6010

# Run the application
CMD ["java", "-jar", "BookService-0.0.1-SNAPSHOT.jar"]
