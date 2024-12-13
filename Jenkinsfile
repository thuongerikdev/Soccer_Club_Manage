pipeline {
    agent any 
    stages {
        stage('Clone Repository') {
            steps {
                git 'https://github.com/thuongerikdev/Soccer_Club_Manage.git'
            }
        }
        stage('Build and Push Docker Image') {
            steps {
                script {
                    def dockerImageName = "emyeuaidayy/sm-soccer-ver1"
                    def dateFormat = new java.text.SimpleDateFormat("yyMMddHHmm") // Định dạng thời gian
                    def currentDate = dateFormat.format(new Date())
                    def dockerTag = "${currentDate}" // Thẻ với thời gian hiện tại
                    
                    withDockerRegistry(credentialsId: 'docker-hub', url: 'https://index.docker.io/v1/') {
                        bat """
                            cd backend/SM.WebAPI
                            docker build -t ${dockerImageName}:${dockerTag} .
                            docker push ${dockerImageName}:${dockerTag}
                        """
                    }
                    
                    // Cài đặt biến môi trường cho docker-compose
                    env.VERSION = dockerTag
                }
            }
        }
        stage('Deploy with Docker Compose') {
            steps {
                script {
                    bat """
                        docker-compose up -d --build
                    """
                }
            }
        }
    }
    post {
        always {
            script {
                // Dọn dẹp nếu cần thiết
                bat 'docker-compose down'
            }
        }
    }
}