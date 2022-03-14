pipeline {
     agent any
     environment {
        dockerImage = ''
        registry = 'ashish0786/employeeregistrationservice'
        registryCredential ='dockerhub_id'
    }
    stages {
        stage ('checkout') {
            steps {
                checkout([$class: 'GitSCM', branches: [[name: '*/main']], extensions: [], userRemoteConfigs: [[credentialsId: 'github_id', url: 'https://github.com/ashish0626/EmployeeRegistrationServiceAPI.git']]])
            }
        }
        stage('Build Docker Images') {
            steps {
                script {
                    dockerImage = docker.build registry
                }
            }
        }
        stage('Upload to DockerHub'){
            steps {
                script {
                        docker.withRegistry('',registryCredential){
                            dockerImage.push()
                        }
                }
            }
        }
        stage('docker stop container') {
         steps {
            sh 'docker ps -f name=pedantic_ganguly  -q | xargs --no-run-if-empty docker container stop'
            sh 'docker container ls -a -fname=pedantic_ganguly -q | xargs -r docker container rm'
         }
       }
       stage('Docker Run') {
        steps {
            script {
                dockerImage.run("-p 8020:80 --rm --name pedantic_ganguly")
                }
            }
        }
    }
}
