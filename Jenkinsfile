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
                withDockerRegistry(credentialsId: 'docker_hub', url: 'https://index.docker.io/v1/') {
                    sh '''
                        docker build -f backend/SM.WebAPI/Dockerfile -t emyeuaidayy/sm-soccer-ver1 .
                        docker push emyeuaidayy/sm-soccer-ver1
                    '''
                }
            }
        }
    }
}
