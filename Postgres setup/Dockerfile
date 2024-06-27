FROM postgres:15
COPY uk_UA.affix /usr/share/postgresql/15/tsearch_data/uk_ua.affix
COPY uk_UA.dict /usr/share/postgresql/15/tsearch_data/uk_ua.dict
COPY ukrainian.stop /usr/share/postgresql/15/tsearch_data/ukrainian.stop
# docker build -t postgres_with_dictionary .
# docker run --name some-postgres -p 5432:5432 -e POSTGRES_PASSWORD=Strong.Pwd-123 -e POSTGRES_USER=SA -d postgres_with_dictionary