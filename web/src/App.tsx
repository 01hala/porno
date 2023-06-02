import React from 'react';
import cookie from 'react-cookies';
import { Layout, Menu, Card, Space, Input, Modal, Button } from 'antd';
import { BellOutlined, UserOutlined, InfoCircleOutlined } from '@ant-design/icons';
import { Md5 } from "ts-md5";
import { request } from './request';
import * as protoc from './ccallgate';
import * as protos from './gatecallc';
import * as common from './common';
import { PageData, requestPageData } from './requestPageData';
import logo from './logo.svg';
import './App.css';

const {  useState  } = React;
const { Search } = Input;
const { Header, Sider, Content, Footer } = Layout;

let account = cookie.load('account');
let password = cookie.load('password');
let name = "";
let mail = "";

const headerStyle = {
  margin: 'auto',
  lineHeight: '64px',
  backgroundColor: 'white',
};

const contentStyle = {
  margin: 'auto',
  textAlign: 'center' as const,
  lineHeight: '64px',
  backgroundColor:'white',
};
const siderStyle = {
  margin: 'auto',
  float: 'right' as const,
  height: 64,
  lineHeight: '64px',
  backgroundColor: 'white',
};
const cardStyle = {
  margin: 'auto',
  marginRight: 0,
  textAlign: 'center' as const,
  width: 200,
  lineHeight: '64px',
  backgroundColor: 'white',
};

function onSearch(value:string) {
  console.log(value);
}

const login_or_create_children = [
  {
    label: 'sign in',
    key: 'sign_in',
  },
  {
    label: 'sign up',
    key: 'sign_up',
  },
];

const login_sign_out = [
  {
    label: 'sign out',
    key: 'sign_out',
  },
];

let data : PageData = new PageData();

let isRequest = false;
function App() {
  const [children, setChildren] = React.useState(login_sign_out);
  const [pageData, setPageData] = React.useState<PageData>(data);

  React.useEffect(() => {
    if (isRequest) {
      return;
    }
    isRequest = true;
    
    let req = null;
    let login_uri = '';
    if (account == null || account == undefined) {
      account = '';
    }
    if (password) {
      req = new protoc.user_login_req();
      req.account = account;
      req.password = password;
      login_uri = 'user_login';

      setChildren(login_sign_out);
    }
    else {
      req = new protoc.guest_req();
      req.account = account;
      login_uri = 'guest_login';

      setChildren(login_or_create_children);
    }

    console.log(login_uri);
    console.log(account);
    requestPageData(login_uri, req).then((data)=>{
      console.log(data);
      if (login_uri == 'guest_login') {
        account = data.account;
      }
      cookie.save('account', account, {path:'/'});
      setPageData(data);
    }).catch((err)=>{
      setModalErrInfo(err);
    });
  }, []);

  function setModalErrInfo(err:common.error) {
    console.log("err:", err);
    if (err == common.error.wrong_password) {
      setErrInfo("password is wrong!");
      setIsModalErrInfoOpen(true);
    }
    else if (err == common.error.account_repeat) {
      setErrInfo("account is repeat!");
      setIsModalErrInfoOpen(true);
    }
    else if (err == common.error.mail_repeat) {
      setErrInfo("mail is repeat!");
      setIsModalErrInfoOpen(true);
    }
  }

  const [current, setCurrent] = useState('home');
  function onClick(e:any) {
    setCurrent(e.key);

    if (e.key == 'sign_in') {
      setIsModalSignInOpen(true);
    }
    else if (e.key == 'sign_up') {
      setIsModalSignUpOpen(true);
    }
    else if (e.key == 'sign_out') {
      account = "";
      password = "";

      let req = new protoc.guest_req();
      req.account = account;
      requestPageData('guest_login', req).then((data)=>{
        
        setChildren(login_or_create_children);

        console.log(data);
        account = data.account;
        cookie.save('account', account, {path:'/'});
        setPageData(data);
      }).catch((err)=>{
        setModalErrInfo(err);
      });
    }
  };

  const [error_info, setErrInfo] = useState("");
  const [IsModalErrInfoOpen, setIsModalErrInfoOpen] = useState(false);
  const handleErrInfoOk = () => {
    setIsModalErrInfoOpen(false);
  };

  const [IsModalSignInOpen, setIsModalSignInOpen] = useState(false);
  const handleSignInAccount = (event:React.ChangeEvent) => {
    let target = event.target as any;
    account = Md5.hashStr(target.value).toString();
  }
  const handleSignInPassword = (event:React.ChangeEvent) => {
    let target = event.target as any;
    password = Md5.hashStr(target.value).toString();
  }
  const handleSignInOk = () => {
    let req = new protoc.user_login_req();
    req.account = account;
    req.password = password;
    requestPageData('user_login', req).then((data)=>{
      setChildren(login_sign_out);

      console.log(data);
      setPageData(data);
      setIsModalSignInOpen(false);
    }).catch((err)=>{
      setIsModalSignInOpen(false);
      setModalErrInfo(err);
    });
  };
  const handleSignInCancel = () => {
    setIsModalSignInOpen(false);
  };

  const [IsModalSignUpOpen, setIsModalSignUpOpen] = useState(false);
  const handleSignUpAccount = (event:React.ChangeEvent) => {
    let target = event.target as any;
    account = Md5.hashStr(target.value).toString();
  }
  const handleSignUpPassword = (event:React.ChangeEvent) => {
    let target = event.target as any;
    password = Md5.hashStr(target.value).toString();
  }
  const handleSignUpName = (event:React.ChangeEvent) => {
    let target = event.target as any;
    name = target.value;
  }
  const handleSignUpMail = (event:React.ChangeEvent) => {
    let target = event.target as any;
    mail = target.value;
  }
  const handleSignUpOk = () => {
    console.log(account)
    console.log(password)
    let req = new protoc.user_create_req();
    req.token = pageData.token;
    req.account = account;
    req.name = name;
    req.mail = mail;
    req.password = password;
    console.log(req);
    request<protos.user_data_rsp>('user_create', req).then((user_data)=>{
      console.log(user_data);
      if (user_data.code != common.error.success) {
        setIsModalSignUpOpen(false);
        setModalErrInfo(user_data.code);
      }
      else {
        if (user_data.data) {
          cookie.save('account', user_data.data.account, {path:'/'});
          cookie.save('password', password, {path:'/'});

          let req = new protoc.user_login_req();
          req.account = user_data.data.account;
          req.password = password;
          requestPageData('user_login', req).then((data)=>{
            setIsModalSignUpOpen(false);
            setPageData(data);
            setChildren(login_sign_out);
          }).catch((err)=>{
            setIsModalSignUpOpen(false);
            setModalErrInfo(err);
          });;
        }
      }
    });
  };
  const handleSignUpCancel = () => {
    setIsModalSignUpOpen(false);
  };

  return <Layout>
          <Header style={headerStyle}> 
           <Menu onClick={onClick} selectedKeys={[current]} mode="horizontal" items={[
              {
                label: 'Home',
                key: 'home',
              },
              {
                label: 'Generate',
                key: 'generate',
              },
              {
                label: (<Search 
                  placeholder='search essay'
                  onSearch={onSearch} 
                  style={{
                    marginLeft: 200,
                    marginTop: 20,
                    width: 260,
                  }}
                />),
                key: 'search',
              },
              {
                label: (<BellOutlined 
                  style={{
                    marginTop: 30,
                  }}
                />),
                key: 'msg',
              },
              {
                label: (
                <UserOutlined style={{ marginTop: 30, }}>
                </UserOutlined>
                ),
                key: 'self',
                children:children
              }]
            } />
           <Layout>
            <Content style={contentStyle}>Content</Content>
            <Sider style={siderStyle}>
             <Space direction="vertical">
              <Card style={cardStyle} size="small" title={pageData.name} >
                <p>coin {pageData.coin}</p>
                <p>level {pageData.level}</p>
              </Card>
             </Space>
            </Sider>
           </Layout>
          </Header> 
          
          <Modal title="Error Info" open={IsModalErrInfoOpen} onOk={handleErrInfoOk} onCancel={handleErrInfoOk}
            footer={[
             <Button key="Ok" onClick={handleErrInfoOk}>Ok</Button>,
            ]}
          >
            <p>{error_info}</p>
          </Modal>

          <Modal title="Sign In" open={IsModalSignInOpen} onOk={handleSignInOk} onCancel={handleSignInCancel}>
            <Input
              style={{
                marginTop: 20,
              }}
              placeholder="Enter your account"
              prefix={<UserOutlined className="site-form-item-icon" />}
              onChange={handleSignInAccount}
            />
            <Input.Password
              style={{
                marginTop: 30,
              }}
              placeholder="Enter your password"
              onChange={handleSignInPassword}
            />
          </Modal>

          <Modal title="Sign Up" open={IsModalSignUpOpen} onOk={handleSignUpOk} onCancel={handleSignUpCancel}>
            <Input
              style={{
                marginTop: 20,
              }}
              placeholder="Enter your account"
              prefix={<UserOutlined className="site-form-item-icon" />}
              onChange={handleSignUpAccount}
            />
            <Input
              style={{
                marginTop: 20,
              }}
              placeholder="Enter your name"
              onChange={handleSignUpName}
            />
            <Input
              style={{
                marginTop: 20,
              }}
              placeholder="Enter your mail"
              onChange={handleSignUpMail}
            />
            <Input.Password
              style={{
                marginTop: 30,
              }}
              placeholder="Enter your password"
              onChange={handleSignUpPassword}
            />
          </Modal>

         </Layout>;
}

export default App;
