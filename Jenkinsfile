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
                    def dateFormat = new java.text.SimpleDateFormat("yyMMddHHmm") // No separators
                    def currentDate = dateFormat.format(new Date())
                    env.DOCKER_TAG = "${currentDate}" // Set environment variable for reuse
                    
                    withDockerRegistry(credentialsId: 'docker-hub', url: 'https://index.docker.io/v1/') {
                        bat """
                            cd backend/SM.WebAPI
                            docker build -t ${dockerImageName}:${env.DOCKER_TAG} .
                            docker push ${dockerImageName}:${env.DOCKER_TAG}
                        """
                    }
                }
            }
        }
        stage('Run') {
            steps {
                script {
                    bat """
                        #set IMAGE_TAG=${env.DOCKER_TAG}
                        docker-compose -f docker-compose.yml up -d
                    """
                }
            }
        }
    }
}
