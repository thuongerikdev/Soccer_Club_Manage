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
                    def dockerImageName = "emyeuaidayy/sm-soccer-ver1"
                    
                    withDockerRegistry(credentialsId: 'docker-hub', url: 'https://index.docker.io/v1/') {
                        bat """
                            cd backend/SM.WebAPI
                            docker build -t ${dockerImageName} .
                            docker push ${dockerImageName}
                        """
                    }
                }
            }
        }
    }
}