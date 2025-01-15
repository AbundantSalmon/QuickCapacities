# Contributing

Changes tracked using [changesets](https://github.com/changesets/changesets) and [knope](https://github.com/knope-dev/knope).

```bash
cargo install knope


# For each PR make sure to run this command to make a changeset
knope document-change

```

When a PR is merged, a github action will automatically run and keep a release PR updated

A release is made when a PR is merged to main
