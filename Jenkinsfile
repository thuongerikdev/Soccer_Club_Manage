pipeline {
    agent any 
    stages {
        stage('Clone') {
            steps {
                git 'https://github.com/thuongerikdev/Soccer_Club_Manage.git'
            }
        }
        stage('Build') {
            steps {
                script {
                    // Use Windows-style commands
                    def dockerImageName = "emyeuaidayy/sm-soccer-ver1"
                    
                    // Authenticate with Docker Hub
                    withDockerRegistry(credentialsId: 'docker-hub', url: 'https://index.docker.io/v1/') {
                        bat '''
                            docker build -f backend/SM.WebAPI/Dockerfile -t %dockerImageName% .
                            docker push %dockerImageName%
                        '''
                    }
                }
            }
        }
    }
}