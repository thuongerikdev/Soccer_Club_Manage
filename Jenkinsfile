pipeline {
    agent any 
    stages {
        stage('Clone') {
            steps {
                git 'https://github.com/thuongerikdev/Soccer_Club_Manage.git'
            }
        }
        stage('Build and Push') {
            steps {
                script {
                    def dockerImageName = "emyeuaidayy/sm-soccer-ver1"
                    def dateFormat = new java.text.SimpleDateFormat("yyMMddHHmm") // No separators
                    def currentDate = dateFormat.format(new Date())
                    def dockerTag = "${currentDate}" // Tag with concatenated date and time
                    
                    withDockerRegistry(credentialsId: 'docker-hub', url: 'https://index.docker.io/v1/') {
                        bat """
                            cd backend/SM.WebAPI
                            docker build -t ${dockerImageName}:${dockerTag} .
                            docker push ${dockerImageName}:${dockerTag}
                        """
                    }
                    
                    // Save the docker image and tag for reuse
                    env.DOCKER_IMAGE = dockerImageName
                    env.DOCKER_TAG = dockerTag
                }
            }
        }
        stage('Deploy with Docker Compose') {
            steps {
                script {
                    // Update docker-compose.yml to use the newly built image
                    def updatedCompose = readFile('docker-compose.yml')
                        .replace('emyeuaidayy/sm-soccer-ver1:2412121230', "${env.DOCKER_IMAGE}:${env.DOCKER_TAG}")
                    writeFile(file: 'docker-compose-updated.yml', text: updatedCompose)
                    
                    bat """
                        docker-compose -f docker-compose-updated.yml down || echo "No existing containers to stop."
                        docker-compose -f docker-compose-updated.yml up -d
                    """
                }
            }
        }
    }
}
