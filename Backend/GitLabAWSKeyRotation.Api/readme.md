This application works as a backend to register AWS Accounts and IAM Identities (Users) to be rotated. When rotating AWS Keys, dependent GitLab projects (its variables) are updated with new credentials.

The IAM Identity needs to have permission to be able to rotate its own keys. Therefore the following permissions must be granted to the IAM User:
```
{
     "Effect": "Allow",
     "Action": [
         "iam:*AccessKey*"
     ],
     "Resource": [
         "arn:aws:iam::*:user/${aws:username}"
     ]
}
```

To start a local developer db:
docker run -d --name GitLabAWSKeyRotationDb -e MARIADB_USER=admin -e MARIADB_PASSWORD=RandomPass0815 -e MARIADB_ROOT_PASSWORD=RandomPass0815  -p 3306:3306 mariadb:latest

Create db "GitLabAWSKeyRotationDb" with utf8mb4 charset and utf8_bin collation