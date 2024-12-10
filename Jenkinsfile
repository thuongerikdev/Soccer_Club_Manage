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
                        cd backend/SM.WebAPI
                        docker build -t emyeuaidayy/SM_Soccer_Ver1 .
                        docker push emyeuaidayy/SM_Soccer_Ver1
                    '''
                }
            }
        }
    }
}
