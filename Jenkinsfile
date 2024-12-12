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
                    def dateFormat = new java.text.SimpleDateFormat("yyyy-MM-dd_HH-mm-ss")
                    def currentDate = dateFormat.format(new Date())
                    def dockerTag = "${currentDate}" // Tag containing the current date and time
                    
                    withDockerRegistry(credentialsId: 'docker-hub', url: 'https://index.docker.io/v1/') {
                        bat """
                            cd backend/SM.WebAPI
                            docker build -t ${dockerImageName}:${dockerTag} .
                            docker push ${dockerImageName}:${dockerTag}
                        """
                    }
                }
            }
        }
    }
}