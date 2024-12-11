pipeline {
    agent any
    stages {
        stage('Checkout') {
            steps {
                checkout([$class: 'GitSCM',
                    branches: [[name: '*/master']],  // Chỉ định nhánh master
                    userRemoteConfigs: [[url: 'https://github.com/thuongerikdev/Soccer_Club_Manage.git']]
                ])
            }
        }
    }
}
