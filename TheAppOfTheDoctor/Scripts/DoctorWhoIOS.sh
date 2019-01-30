#!/bin/bash
echo "-----START SCRIPT-----";
echo "Configuration build:$BUILDCONFIGURATION";

if [ $BUILDCONFIGURATION = 'Release' ]
then
    echo "##vso[task.setvariable variable=appcenterid;]id-de-appcenter";
    echo "##vso[task.setvariable variable=AppSlug;]name-key-de-appcenter";
    echo "Variable setted.";
elif [ $BUILDCONFIGURATION = 'DOCTOR10' ]
then    
    echo "##vso[task.setvariable variable=appcenterid;]id-de-appcenter";
    echo "##vso[task.setvariable variable=AppSlug;]name-key-de-appcenter";
    echo "Variable setted.";
elif [ $BUILDCONFIGURATION = 'DOCTOR11' ]
then    
    echo "##vso[task.setvariable variable=appcenterid;]id-de-appcenter";
    echo "##vso[task.setvariable variable=AppSlug;]name-key-de-appcenter";
    echo "Variable setted.";
elif [ $BUILDCONFIGURATION = 'DOCTOR12' ]
then    
    echo "##vso[task.setvariable variable=appcenterid;]id-de-appcenter";
    echo "##vso[task.setvariable variable=AppSlug;]name-key-de-appcenter";
    echo "Variable setted.";
elif [ $BUILDCONFIGURATION = 'DOCTOR13' ]
then    
    echo "##vso[task.setvariable variable=appcenterid;]id-de-appcenter";
    echo "##vso[task.setvariable variable=AppSlug;]name-key-de-appcenter";
    echo "Variable setted.";
else
    echo 'Environment not recognized';
fi

echo "-----END SCRIPT-----";