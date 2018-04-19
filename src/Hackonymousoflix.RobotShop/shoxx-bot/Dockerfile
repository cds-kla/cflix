FROM zopanix/casperjs
COPY script.js .
CMD [ "--ignore-ssl-errors=true", \
      "--local-to-remote-url-access=true", \
      "--web-security=false", \
      "--ssl-protocol=any", \
      "--log-level=error", \
      "script.js"]
