if [ -n "$CODECOV_REPO_TOKEN" ]
then
  curl -s https://codecov.io/bash > codecov
  chmod +x codecov
  ./codecov -f coverage/coverage.xml -t $CODECOV_REPO_TOKEN
fi
